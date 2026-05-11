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
}