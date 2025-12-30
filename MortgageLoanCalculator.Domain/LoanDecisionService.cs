namespace MortgageLoanCalculator.Domain
{
    public class LoanDecisionService
    {
        public LoanDecisionResult DetermineEligibility(double monthlyPaymentTotalWithFees, double annualIncome)
        {
            string decision;
            string reason;

            if (monthlyPaymentTotalWithFees >= (annualIncome / 12) * 0.25)
            {
                decision = "DENY";
                reason = "Monthly payment exceeds 25% of monthly income";
            }
            else
            {
                decision = "APPROVE";
                reason = "Monthly payment is lower than 25% of monthly income";
            }
            LoanDecisionResult loanDecisionResult = new LoanDecisionResult()
            {
                Decision = decision,
                Reason = reason
            };
            return loanDecisionResult;
        }
    }
}