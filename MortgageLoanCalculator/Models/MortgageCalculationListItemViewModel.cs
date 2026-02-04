using MortgageLoanCalculator.Domain;

namespace MortgageLoanCalculator.Models
{
    public class MortgageCalculationListItemViewModel
    {
        public int SnapshotId { get; set; }

        public double PurchasePrice { get; set; }
        public double DownPayment { get; set; }
        public double AnnualInterestRate { get; set; }
        public LoanTerm Term { get; set; }

        public double TotalMonthlyPayment { get; set; }
        public double PaymentIncomeRatio { get; set; }
        public string Decision { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
