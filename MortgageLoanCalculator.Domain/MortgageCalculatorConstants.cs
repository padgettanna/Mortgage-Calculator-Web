namespace MortgageLoanCalculator.Domain
{
    public class MortgageCalculatorConstants
    {
        public const double OriginationFeePercentage = 0.01;
        public const double LoanInsurancePercentage = 0.01;
        public const double ClosingCosts = 2500;
        public const double AnnualPropertyTaxPercentage = 0.0125;
        public const double AnnualHomeownersInsurancePercentage = 0.0075;
        public const double MinAnnualIncome = 10000;
        public const double MaxAnnualIncome = 9999999;
        public const double MinPropertyPrice = 20000;
        public const double MaxPropertyPrice = 900000000;
        public const double MinDownPayment = 5000;
        public const double MaxDownPayment = 100000;
        public const double MinInterestRate = 0.1;
        public const double MaxInterestRate = 30;
        public const double MaxAnnualHoaFee = 20000;
    }
}