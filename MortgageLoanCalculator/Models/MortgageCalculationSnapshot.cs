using Microsoft.AspNetCore.Identity;
using MortgageLoanCalculator.Domain;

namespace MortgageLoanCalculator.Models
{
    public class MortgageCalculationSnapshot
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public double AnnualIncome { get; set; }
        public double MarketValue { get; set; }
        public double PurchasePrice { get; set; }
        public double DownPayment { get; set; }
        public double AnnualInterestRate { get; set; }
        public double MonthlyHoaFee { get; set; }
        public PaymentsPerYear NumPaymentsPerYear { get; set; }
        public LoanTerm Term { get; set; }
        public double LoanAmount { get; set; }
        public double MonthlyPayment { get; set; }
        public double TotalMonthlyPayment { get; set; }
        public double PaymentIncomeRatio { get; set; }
        public string Decision { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
