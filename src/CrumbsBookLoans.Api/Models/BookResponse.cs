using CrumbsBookLoans.Api.Entities;

namespace CrumbsBookLoans.Api.Models;

// Named BookResponse rather than BookWithId (per spec) — mirrors CreateBookRequest
// and clearly signals this is an API response shape.
public class BookResponse
{
    public int Id { get; set; }
    public string? Title { get; set; }
    public string? Author { get; set; }
    public string? Isbn { get; set; }
    public DateOnly? PublishedDate { get; set; }
    public string? Owner { get; set; }
    public string LoanStatus { get; set; } = "available";
    public string? Borrower { get; set; }

    public static BookResponse FromBook(Book book) => new()
    {
        Id = book.Id,
        Title = book.Title,
        Author = book.Author,
        Isbn = book.Isbn,
        PublishedDate = book.PublishedDate,
        Owner = book.Owner,
        LoanStatus = book.LoanStatus == Entities.LoanStatus.Borrowed ? "borrowed" : "available",
        Borrower = book.Borrower
    };
}
