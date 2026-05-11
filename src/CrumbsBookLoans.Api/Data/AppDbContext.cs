using CrumbsBookLoans.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrumbsBookLoans.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Book> Books => Set<Book>();
}
