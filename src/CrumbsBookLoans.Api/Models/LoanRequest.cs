using System.ComponentModel.DataAnnotations;

namespace CrumbsBookLoans.Api.Models;

public record LoanRequest([Required] string Borrower);
