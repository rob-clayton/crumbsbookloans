using CrumbsBookLoans.Api.Data;
using CrumbsBookLoans.Api.Entities;
using CrumbsBookLoans.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CrumbsBookLoans.Api.Controllers;

// Testing approach (not written given 1-hour constraint):
// - GET: returns all books mapped to BookResponse; returns empty array when no books exist
// - POST: creates book from request, returns 201 with BookResponse body
// - DELETE: returns 204 on success, 404 when id not found
// - POST loan: sets borrower and status, returns 409 if already borrowed, 404 if not found
// - POST return: clears borrower and resets status to available, 404 if not found

[ApiController]
[Route("api/[controller]")]
public class BooksController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await db.Books.ToListAsync();
        return Ok(books.Select(BookResponse.FromBook));
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateBookRequest request)
    {
        var book = new Book
        {
            Title = request.Title,
            Author = request.Author,
            Isbn = request.Isbn,
            PublishedDate = request.PublishedDate,
            Owner = request.Owner
        };

        db.Books.Add(book);
        await db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAll), new { id = book.Id }, BookResponse.FromBook(book));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null) return NotFound();

        db.Books.Remove(book);
        await db.SaveChangesAsync();

        return NoContent();
    }

    [HttpPost("{id}/loan")]
    public async Task<IActionResult> Loan(int id, LoanRequest request)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null) return NotFound();
        if (book.LoanStatus == LoanStatus.Borrowed)
            return Conflict(new { message = "Book is already borrowed." });

        book.LoanStatus = LoanStatus.Borrowed;
        book.Borrower = request.Borrower;
        await db.SaveChangesAsync();

        return Ok(BookResponse.FromBook(book));
    }

    [HttpPost("{id}/return")]
    public async Task<IActionResult> Return(int id)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null) return NotFound();

        if (book.LoanStatus != LoanStatus.Borrowed)
            return Conflict(new { message = "Book is not currently borrowed." });

        book.LoanStatus = LoanStatus.Available;
        book.Borrower = null;
        await db.SaveChangesAsync();

        return Ok(BookResponse.FromBook(book));
    }
}
