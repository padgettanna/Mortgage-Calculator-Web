namespace MortgageLoanCalculator.Domain
{
    /// <summary>
    /// Determines mortgage loan eligibility based on payment-to-income ratio.
    /// Uses a 25% monthly payment threshold for approval decisions.
    /// </summary>
    public class LoanDecisionService
    {
        public LoanDecisionResult DetermineEligibility(double monthlyPaymentTotalWithFees, double annualIncome)
        {
            string decision;
            string reason;

            // Deny if monthly payment exceeds 25% of monthly income
            if (monthlyPaymentTotalWithFees >= (annualIncome / MortgageCalculatorConstants.MonthsPerYear) * MortgageCalculatorConstants.MaxMonthlyPaymentToIncomeRatio)
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