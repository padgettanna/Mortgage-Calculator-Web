using System.ComponentModel.DataAnnotations;

namespace MortgageLoanCalculator.Domain;

public enum PaymentsPerYear : int
{
    [Display(Name = "4 (Quarterly)")]
    Quarterly = 4,

    [Display(Name = "12 (Monthly)")]
    Monthly = 12,

    [Display(Name = "24 (Bi-weekly)")]
    Biweekly = 24

}
