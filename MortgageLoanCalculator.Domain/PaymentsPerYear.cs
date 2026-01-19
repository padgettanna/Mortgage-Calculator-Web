using System.ComponentModel.DataAnnotations;

namespace MortgageLoanCalculator.Domain;

/// <summary>
/// Represents the frequency of loan payments per year.
/// </summary>
public enum PaymentsPerYear : int
{
    [Display(Name = "4 (Quarterly)")]
    Quarterly = 4,

    [Display(Name = "12 (Monthly)")]
    Monthly = 12,

    [Display(Name = "24 (Semi-monthly)")]
    SemiMonthly = 24

}
