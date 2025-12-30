using MortgageLoanCalculator.Domain;


namespace MortgageLoanCalculator.Models
{
    public class MortgageResultViewModel
    {
        public string BorrowerName { get; set; }
        public double AnnualIncome { get; set; }
        public double PurchasePrice { get; set; }
        public double DownPayment { get; set; }
        public double AnnualInterestRate { get; set; }
        public double MonthlyHoaFee { get; set; }
        public double MonthlyPaymentIncomeRatio { get; set; }
        public LoanTerm Term { get; set; }
        public LoanCalculationResult Calculation { get; set; }
        public LoanDecisionResult Decision { get; set; }
    }
}
