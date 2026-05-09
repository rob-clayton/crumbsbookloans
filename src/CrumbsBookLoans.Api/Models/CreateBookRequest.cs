using System.ComponentModel.DataAnnotations;

namespace CrumbsBookLoans.Api.Models;

// Named CreateBookRequest rather than Book (per spec) — request/response naming
// makes the intent explicit at API boundaries.
public class CreateBookRequest
{
    [Required, MinLength(1)] public string Title { get; set; } = "";
    public string? Author { get; set; }
    public string? Isbn { get; set; }
    public DateOnly? PublishedDate { get; set; }
    public string? Owner { get; set; }
}
