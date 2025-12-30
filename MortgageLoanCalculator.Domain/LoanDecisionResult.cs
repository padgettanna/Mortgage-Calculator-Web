namespace MortgageLoanCalculator.Domain
{
    // Represents the calculated outcome of a mortgage evaluation
    public class LoanDecisionResult
    {
        public string Decision { get; init; }
        public string Reason { get; init; }
    }
}