using CrumbsBookLoans.Api.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Data Source=bookloans.db"));

// CORS policy for development - allows requests from React dev server
builder.Services.AddCors(options =>
    options.AddPolicy("DevCors", policy =>
        policy.WithOrigins("http://localhost:5173")
              .AllowAnyMethod()
              .AllowAnyHeader()));

var app = builder.Build();

// Apply any pending migrations automatically on startup
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<AppDbContext>().Database.Migrate();

// In development, serve OpenAPI docs and allow CORS for React dev server; in production, serve React SPA and API from same origin
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Map Scalar API reference for development - this will be available at /scalar/v1
    app.MapScalarApiReference();
    app.UseCors("DevCors");
}

// Use HTTPS redirection, serve static files (for React SPA), and set up routing and authorization
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthorization();
app.MapControllers();

// Serve React SPA for any non-API route in production
app.MapFallbackToFile("index.html");

app.Run();
