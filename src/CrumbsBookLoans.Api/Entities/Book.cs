using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CrumbsBookLoans.Api.Entities;

public enum LoanStatus { Available, Borrowed }

[Index(nameof(Title))]
[Index(nameof(Isbn), IsUnique = true)]
[Index(nameof(Id), IsUnique = true)]
public class Book
{
    [Key] // I know this is redundant since EF Core will treat "Id" as the primary key by convention, but I want to be explicit
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public string? Author { get; set; }
    public string? Isbn { get; set; }
    public DateOnly? PublishedDate { get; set; }
    public string? Owner { get; set; }
    public LoanStatus LoanStatus { get; set; } = LoanStatus.Available;
    public string? Borrower { get; set; }
}
