using CrumbsBookLoans.Api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CrumbsBookLoans.Api.Tests;

/// <summary>
/// Custom WebApplicationFactory for integration testing the API with an in-memory SQLite database.
/// Builds an in-memory database built from the same schema as the real database, but without any persisted data.
/// So no pollution between tests, and no need to clean up after tests. Each test gets a fresh database instance that is created on demand when the test server starts up.
/// For each test within the test class the database will be shared, so we will need to ensure that no pollution occurs WITHIN the test class.
/// Each test will get a fresh in-memory database instance, ensuring isolation and repeatability of tests.
/// </summary>
public class TestWebApplicationFactory : WebApplicationFactory<Program>
{
    // I thought about creating a shared DBContext between the web api and the tests and starting a transaction on the connection
    // and rolling back after each test, but that would be more complex and I really couldn't be bothered.
    // It also has it's own issues with ensuring that the transaction is properly rolled back after each test, and that the connection is properly shared between the web api and the tests, and
    // that the transaction is properly started before any database operations occur in the web api, etc.
    // Also, in more complicated systems, multiple database connections might be used, and ensuring that all of them are properly shared and have transactions started on them would be a nightmare.

    private readonly SqliteConnection _connection = new("Data Source=:memory:");

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        // Set environment to "Testing" so we can perform environment-specific configuration in the API if needed (migrations on startup when not in testing environment, etc)
        builder.UseEnvironment("Testing");

        _connection.Open();

        builder.ConfigureServices(services =>
        {
            // Remove the real DbContext registration
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType == typeof(DbContextOptions<AppDbContext>));
            if (descriptor != null) services.Remove(descriptor);

            // Replace with in-memory SQLite
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite(_connection));

            // Ensure schema is created
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            db.Database.EnsureCreated();
        });
    }
    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _connection.Dispose();
    }

}