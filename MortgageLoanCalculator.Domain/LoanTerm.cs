using System.ComponentModel.DataAnnotations;

namespace MortgageLoanCalculator.Domain;

/// <summary>
/// Represents the duration of a mortgage loan term in years.
/// </summary>
public enum LoanTerm : int
{
    [Display(Name = "15 Years")]
    FifteenYears = 15,

    [Display(Name = "30 Years")]
    ThirtyYears = 30
}