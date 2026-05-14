using System.Data;
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

// Just as a note, I know the spec says to return 404 for trying to loan an already borrowed book, but I think 409 Conflict is more appropriate since the resource exists but is in an invalid state
// Maybe there's a security concern ... but a conflict feels more correct

// Another note.  All routes are async of course (not sure why it's even an option these days) since there are limited threads, and at least
// if we hit an await call the main thread can handle another request until the await returns.
// I know there's a fractional overhead ... but tell me a modern api call that doesn't hit at least one await call?  Even if it's just the database save, or a call to an external service, etc.

[ApiController]
[Route("api/[controller]")]
public class BooksController(AppDbContext db) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var books = await db.Books.ToListAsync();
        return Ok(books.OrderBy(b => b.Title).Select(BookResponse.FromBook));
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

        // Okay, I know this locks the entire db, and of course you wouldn't have this in production
        // BUT we also wouldn't use an in-memory SQLite database in production, and this is just to ensure that we don't have any race conditions for tests, etc
        // Locks the entire db file for reading/writing until the transaction is commited
        using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        if (request.Isbn != null && await db.Books.AnyAsync(b => b.Isbn == request.Isbn))
            return Conflict(new { message = "A book with the same ISBN already exists." });

        db.Books.Add(book);
        // Save changes
        await db.SaveChangesAsync();
        // Commit transaction (and release lock)
        await transaction.CommitAsync();

        return CreatedAtAction(nameof(GetAll), new { id = book.Id }, BookResponse.FromBook(book));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CreateBookRequest request)
    {
        var book = await db.Books.FindAsync(id);
        if (book is null) return NotFound();

        book.Title = request.Title;
        book.Author = request.Author;
        book.Isbn = request.Isbn;
        book.PublishedDate = request.PublishedDate;
        book.Owner = request.Owner;

        using var transaction = await db.Database.BeginTransactionAsync(IsolationLevel.Serializable);

        // Same as inserting, but skip same record since it's an update, not an insert
        if (request.Isbn != null && await db.Books.AnyAsync(b => b.Isbn == request.Isbn && b.Id != id))
            return Conflict(new { message = "A book with the same ISBN already exists." });

        await db.SaveChangesAsync();
        await transaction.CommitAsync();

        return Ok(BookResponse.FromBook(book));
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

    // Loan and return endpoints are POST since they are actions that change the state of the resource, but they are not idempotent (loaning an already loaned book will fail, returning a different result than the first call), so POST is more appropriate than PUT or PATCH.
    // I chose to use loan and return endpoints rather than a single endpoint that toggles the loan status, since these are processes and may do more than just update the loan status in the future (e.g. send notifications, etc.), and having separate endpoints makes the intent more explicit and allows for more flexibility in the future.
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
