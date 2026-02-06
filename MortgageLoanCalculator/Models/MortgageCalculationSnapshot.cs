using Microsoft.AspNetCore.Identity;
using MortgageLoanCalculator.Domain;

namespace MortgageLoanCalculator.Models
{
    /// <summary>
    /// Represents a persisted snapshot of a mortgage calculation.
    /// Stores both user inputs and computed results to support
    /// historical review, comparison, and adjustment.
    /// </summary>
    public class MortgageCalculationSnapshot
    {
        // Primary key for the snapshot
        public int Id { get; set; }

        // Identity user association to ensure calculations are user-specific
        public string UserId { get; set; } = string.Empty;
        public IdentityUser User { get; set; }

        // User-provided loan inputs
        public double AnnualIncome { get; set; }
        public double MarketValue { get; set; }
        public double PurchasePrice { get; set; }
        public double DownPayment { get; set; }
        public double AnnualInterestRate { get; set; }
        public double MonthlyHoaFee { get; set; }
        public PaymentsPerYear NumPaymentsPerYear { get; set; }
        public LoanTerm Term { get; set; }

        // Calculated loan results persisted to preserve historical accuracy
        public double LoanAmount { get; set; }
        public double MonthlyPayment { get; set; }
        public double TotalMonthlyPayment { get; set; }
        public double PaymentIncomeRatio { get; set; }
        public string Decision { get; set; }

        // Timestamp used for ordering and historical tracking
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
