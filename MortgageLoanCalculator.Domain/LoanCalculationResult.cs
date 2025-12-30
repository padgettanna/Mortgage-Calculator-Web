namespace MortgageLoanCalculator.Domain
{
    // Represents the calculated outcome of a mortgage evaluation
    public class LoanCalculationResult
    {
        public double LoanAmount { get; init; }
        public double OriginationFee { get; init; }
        public double ClosingCosts { get; init; }
        public double EquityValue { get; init; }
        public double EquityPercentage { get; init; }
        public int TotalPaymentsPerLoan { get; init; }
        public double LoanInsurancePerMonth { get; init; }
        public double PropertyTaxPerMonth { get; init; }
        public double HomeownersInsurancePerMonth { get; init; }
        public double MonthlyPaymentPI { get; init; }
        public double MonthlyPaymentTotalWithFees { get; init; }
    }
}