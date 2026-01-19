namespace MortgageLoanCalculator.Domain
{
    /// <summary>
    /// Represents the calculated outcome of a mortgage loan evaluation.
    /// Contains all computed values including payments, fees, and equity information.
    /// </summary>
    public class LoanCalculationResult
    {
        public double LoanAmount { get; init; }
        public double OriginationFee { get; init; }
        public double ClosingCosts { get; init; }
        public double EquityValue { get; init; }  // Market value - loan amount
        public double EquityPercentage { get; init; }  // Used to determine if PMI is required
        public int TotalPaymentsPerLoan { get; init; }
        public double LoanInsurancePerMonth { get; init; }  // PMI (only if equity < 10%)
        public double PropertyTaxPerMonth { get; init; }
        public double HomeownersInsurancePerMonth { get; init; }
        public double MonthlyPaymentPI { get; init; }  // Principal and interest only
        public double MonthlyPaymentTotalWithFees { get; init; }  // Total monthly payment including all fees
    }
}