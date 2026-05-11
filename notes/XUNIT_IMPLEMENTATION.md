# xUnit Integration Testing

## What was set up

Test project created and wired into the solution:

```bash
# Create the xUnit test project
dotnet new xunit -n CrumbsBookLoans.Api.Tests -o src/CrumbsBookLoans.Api.Tests

# Create a new solution at src/ level (cleaner than leaving it inside the API folder)
dotnet new sln -n CrumbsBookLoans -o src/

# Add both projects
dotnet sln src/CrumbsBookLoans.slnx add src/CrumbsBookLoans.Api/CrumbsBookLoans.Api.csproj
dotnet sln src/CrumbsBookLoans.slnx add src/CrumbsBookLoans.Api.Tests/CrumbsBookLoans.Api.Tests.csproj

# Add project reference so tests can see the API
dotnet add src/CrumbsBookLoans.Api.Tests reference src/CrumbsBookLoans.Api/CrumbsBookLoans.Api.csproj
```

---

## Adding integration tests

Integration tests spin up the full ASP.NET Core pipeline in-process using `WebApplicationFactory`. This is the right approach here — the controllers are thin wrappers over EF Core, so there's not much to unit test in isolation. The interesting behaviour is at the HTTP boundary.

### 1. Add the testing package

```bash
dotnet add src/CrumbsBookLoans.Api.Tests package Microsoft.AspNetCore.Mvc.Testing
```

### 2. Make Program accessible to the test project

`WebApplicationFactory<Program>` needs to reference `Program`. By default it's internal to the API assembly. Two options:

**Option A — add InternalsVisibleTo to the API project:**

In `CrumbsBookLoans.Api.csproj`:
```xml
<ItemGroup>
  <InternalsVisibleTo Include="CrumbsBookLoans.Api.Tests" />
</ItemGroup>
```

**Option B — make Program a public partial class:**

At the bottom of `Program.cs`:
```csharp
public partial class Program { }
```

Option B is simpler and more common.

### 3. Configure a test database

Override the DbContext registration in your factory to use an in-memory SQLite database so tests don't touch the dev database:

```csharp
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            // Remove the real DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            // Replace with in-memory SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=:memory:"));

            // Ensure schema is created
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }
}
```

### 4. Write tests

```csharp
public class BooksControllerTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BooksControllerTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

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
        var newBook = new { title = "Test Book", author = "Test Author", owner = "Rob" };

        var response = await _client.PostAsJsonAsync("/api/books", newBook);

        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    [Fact]
    public async Task PostBook_MissingTitle_ReturnsBadRequest()
    {
        var newBook = new { author = "Test Author", owner = "Rob" };

        var response = await _client.PostAsJsonAsync("/api/books", newBook);

        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task LoanBook_UpdatesBorrowedStatus()
    {
        // Add a book first
        var created = await _client.PostAsJsonAsync("/api/books",
            new { title = "Book", author = "Author", owner = "Rob" });
        var book = await created.Content.ReadFromJsonAsync<BookResponse>();

        var response = await _client.PostAsJsonAsync(
            $"/api/books/{book!.Id}/loan", new { borrower = "Jess" });

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}
```

### 5. Run the tests

```bash
dotnet test src/CrumbsBookLoans.slnx
```

---

## Optional: FluentAssertions

The `.Should().Be(...)` syntax above uses FluentAssertions — more readable than raw xUnit asserts. Add it with:

```bash
dotnet add src/CrumbsBookLoans.Api.Tests package FluentAssertions
```
