namespace MortgageLoanCalculator.Domain
{
    /// <summary>
    /// Provides constant values for mortgage calculation business rules and validation limits.
    /// </summary>
    public class MortgageCalculatorConstants
    {
        // Fee percentages and fixed costs
        public const double OriginationFeePercentage = 0.01;  // 1%
        public const double LoanInsurancePercentage = 0.01;   // 1% (PMI when equity < 10%)
        public const double ClosingCosts = 2500;
        public const double AnnualPropertyTaxPercentage = 0.0125;  // 1.25%
        public const double AnnualHomeownersInsurancePercentage = 0.0075;  // 0.75%
        public const double MaxMonthlyPaymentToIncomeRatio = 0.25;  // 25%
        public const int MonthsPerYear = 12;
        public const double MinEquityPercentageWithoutPMI = 10.0;

        // Validation limits
        public const double MinAnnualIncome = 10000;
        public const double MaxAnnualIncome = 9999999;
        public const double MinPropertyPrice = 20000;
        public const double MaxPropertyPrice = 900000000;
        public const double MinDownPayment = 5000;
        public const double MaxDownPayment = 100000;
        public const double MinInterestRate = 0.1;  // As percentage
        public const double MaxInterestRate = 30;   // As percentage
        public const double MaxAnnualHoaFee = 20000;
    }
}