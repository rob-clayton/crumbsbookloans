using System.Net;
using System.Net.Http.Json;
using CrumbsBookLoans.Api.Data;
using CrumbsBookLoans.Api.Models;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace CrumbsBookLoans.Api.Tests.Integration;

public class BooksControllerTests : IClassFixture<TestWebApplicationFactory>, IAsyncLifetime
{
    private readonly HttpClient _client;
    private readonly TestWebApplicationFactory _factory;

    public BooksControllerTests(TestWebApplicationFactory factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    // Implement IAsyncLifetime to ensure that the database is cleaned before each test class runs, ensuring isolation between test classes.
    // Like constructor and IDisposable, but async
    // Runs for each test due to 
    public async Task InitializeAsync()
    {
        using var scope = _factory.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Books.RemoveRange(db.Books);
        await db.SaveChangesAsync();
    }

    // Nothing to cleanup
    public Task DisposeAsync() => Task.CompletedTask;


    [Fact]
    public async Task GetBooks_ReturnsOkWithList()
    {
        var response = await _client.GetAsync("/api/books");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var books = await response.Content.ReadFromJsonAsync<List<BookResponse>>();
        books.Should().NotBeNull();
    }

    [Fact]
    public async Task PostBook_ValidData_ReturnsCreated()
    {
        var newBook = new { title = "Test Book", author = "Test Author", owner = "Rob", Isbn = "1234567890", publishedDate = new DateOnly(2020, 1, 1) };

        var response = await _client.PostAsJsonAsync("/api/books", newBook);

        var book = await response.Content.ReadFromJsonAsync<BookResponse>();
        response.StatusCode.Should().Be(HttpStatusCode.Created);

        book.Should().NotBeNull();
        book.Title.Should().Be(newBook.title);
        book.Author.Should().Be(newBook.author);
        book.Owner.Should().Be(newBook.owner);
        book.Isbn.Should().Be(newBook.Isbn);
        book.PublishedDate.Should().Be(newBook.publishedDate);

        book.Id.Should().BeGreaterThan(0);
    }

    // Test creating a book with a duplicate ISBN returns a conflict
    [Fact]
    public async Task PostBook_DuplicateIsbn_ReturnsConflict()
    {
        var newBook = new { title = "Test Book", author = "Test Author", owner = "Rob", isbn = "978-0879977382", publishedDate = new DateOnly(2020, 1, 1) };

        // Insert book twice to create a duplicate ISBN 
        await _client.PostAsJsonAsync("/api/books", newBook);
        var response = await _client.PostAsJsonAsync("/api/books", newBook);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    // Test updating a book with a duplicate ISBN returns a conflict
    [Fact]
    public async Task PutBook_DuplicateIsbn_ReturnsConflict()
    {
        var bookA = new { title = "Book A", author = "Author A", owner = "Rob", isbn = "978-0000000001", publishedDate = new DateOnly(2020, 1, 1) };
        var bookB = new { title = "Book B", author = "Author B", owner = "Rob", isbn = "978-0000000002", publishedDate = new DateOnly(2020, 1, 1) };

        // Insert two books with different ISBNs, then try to update book B with the same ISBN as book A
        await _client.PostAsJsonAsync("/api/books", bookA);
        var createB = await _client.PostAsJsonAsync("/api/books", bookB);
        var createdB = await createB.Content.ReadFromJsonAsync<BookResponse>();

        var update = new { title = "Book B", author = "Author B", owner = "Rob", isbn = bookA.isbn, publishedDate = new DateOnly(2020, 1, 1) };
        var response = await _client.PutAsJsonAsync($"/api/books/{createdB!.Id}", update);

        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    // Test updating a book with the same ISBN (not changing the ISBN) returns OK
    [Fact]
    public async Task PutBook_SameIsbn_ReturnsOk()
    {
        var book = new { title = "Book A", author = "Author A", owner = "Rob", isbn = "978-0000000003", publishedDate = new DateOnly(2020, 1, 1) };

        var created = await (await _client.PostAsJsonAsync("/api/books", book)).Content.ReadFromJsonAsync<BookResponse>();

        var update = new { title = "Book A Updated", author = "Author A", owner = "Rob", isbn = book.isbn, publishedDate = new DateOnly(2020, 1, 1) };
        var response = await _client.PutAsJsonAsync($"/api/books/{created!.Id}", update);

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}