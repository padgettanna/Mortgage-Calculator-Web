namespace MortgageLoanCalculator.Domain
{
    /// <summary>
    /// Represents the outcome of a loan eligibility decision.
    /// </summary>
    public class LoanDecisionResult
    {
        public string Decision { get; init; }  // "APPROVE" or "DENY"
        public string Reason { get; init; }    // Explanation for the decision
    }
}