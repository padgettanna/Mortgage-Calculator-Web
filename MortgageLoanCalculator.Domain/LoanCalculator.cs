namespace MortgageLoanCalculator.Domain
{
    /// <summary>
    /// Provides mortgage loan calculation services including payment schedules, fees, and equity computations.
    /// All calculations assume that origination fees and closing costs are financed into the loan principal.
    /// </summary>
    public class LoanCalculator
    {
        // Formula: (purchasePrice - downPayment) * (1 + OriginationFeePercentage) + ClosingCosts
        private double CalculateLoanAmount(double purchasePrice, double downPayment)
        {
            return ((purchasePrice - downPayment) * MortgageCalculatorConstants.OriginationFeePercentage) + (purchasePrice - downPayment) + MortgageCalculatorConstants.ClosingCosts;
        }

        private double CalculateOriginationFee(double purchasePrice, double downPayment)
        {
            return (purchasePrice - downPayment) * MortgageCalculatorConstants.OriginationFeePercentage;
        }

        private double CalculateEquityValue(double marketValue, double purchasePrice, double downPayment)
        {
            return marketValue - CalculateLoanAmount(purchasePrice, downPayment);
        }

        // If less than 10%, loan insurance (PMI) is required
        private double CalculateEquityPercentage(double marketValue, double purchasePrice, double downPayment)
        {
            return CalculateEquityValue(marketValue, purchasePrice, downPayment) / marketValue * 100;
        }

        private double CalculateMonthlyInterestRate(double annualInterestRate, int numPaymentsPerYear)
        {
            return (annualInterestRate / 100) / numPaymentsPerYear;
        }

        private int CalculateTotalPaymentsPerLoan(int numPaymentsPerYear, int term)
        {
            return numPaymentsPerYear * term;
        }

        // Returns 0 if equity percentage is 10% or greater
        private double CalculateLoanInsurancePerMonth(double marketValue, double purchasePrice, double downPayment, int numPaymentsPerYear)
        {
            double loanInsurancePerMonth = 0;
            if (CalculateEquityPercentage(marketValue, purchasePrice, downPayment) < MortgageCalculatorConstants.MinEquityPercentageWithoutPMI)
            {
                loanInsurancePerMonth = CalculateLoanAmount(purchasePrice, downPayment) * MortgageCalculatorConstants.LoanInsurancePercentage / numPaymentsPerYear;
            }
            return loanInsurancePerMonth;
        }

        private double CalculatePropertyTaxPerMonth(double marketValue, int numPaymentsPerYear)
        {
            return marketValue * MortgageCalculatorConstants.AnnualPropertyTaxPercentage / numPaymentsPerYear;
        }

        private double CalculateHomeownersInsurancePerMonth(double marketValue, int numPaymentsPerYear)
        {
            return marketValue * MortgageCalculatorConstants.AnnualHomeownersInsurancePercentage / numPaymentsPerYear;
        }

        // Uses standard amortization formula: P * [r(1 + r)^n] / [(1 + r)^n - 1]
        private double CalculateMonthlyPaymentPI(Loan loan)
        {
            double growthFactor = Math.Pow(1 + CalculateMonthlyInterestRate(loan.AnnualInterestRate.Value, loan.NumPaymentsPerYear), CalculateTotalPaymentsPerLoan(loan.NumPaymentsPerYear, (int)loan.Term));
            return CalculateLoanAmount(loan.PurchasePrice.Value, loan.DownPayment.Value) * (CalculateMonthlyInterestRate(loan.AnnualInterestRate.Value, loan.NumPaymentsPerYear) * growthFactor) / (growthFactor - 1);
        }

        private double CalculateMonthlyPaymentTotalWithFees(Loan loan)
        {
            return CalculateMonthlyPaymentPI(loan) + CalculateLoanInsurancePerMonth(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value, loan.NumPaymentsPerYear) 
                    + (loan.AnnualHoaFee.Value / loan.NumPaymentsPerYear) + CalculatePropertyTaxPerMonth(loan.MarketValue.Value, loan.NumPaymentsPerYear) 
                    + CalculateHomeownersInsurancePerMonth(loan.MarketValue.Value, loan.NumPaymentsPerYear);
        }

        public LoanCalculationResult CalculateResult(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }

            LoanCalculationResult loanCalculationResult = new LoanCalculationResult()
            {
                LoanAmount = CalculateLoanAmount(loan.PurchasePrice.Value, loan.DownPayment.Value),
                OriginationFee = CalculateOriginationFee(loan.PurchasePrice.Value, loan.DownPayment.Value),
                ClosingCosts = MortgageCalculatorConstants.ClosingCosts,
                TotalPaymentsPerLoan = CalculateTotalPaymentsPerLoan(loan.NumPaymentsPerYear, (int)loan.Term),
                EquityPercentage = CalculateEquityPercentage(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value),
                EquityValue = CalculateEquityValue(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value),
                LoanInsurancePerMonth = CalculateLoanInsurancePerMonth(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value, loan.NumPaymentsPerYear),
                PropertyTaxPerMonth = CalculatePropertyTaxPerMonth(loan.MarketValue.Value, loan.NumPaymentsPerYear),
                HomeownersInsurancePerMonth = CalculateHomeownersInsurancePerMonth(loan.MarketValue.Value, loan.NumPaymentsPerYear),
                MonthlyPaymentPI = CalculateMonthlyPaymentPI(loan),
                MonthlyPaymentTotalWithFees = CalculateMonthlyPaymentTotalWithFees(loan),
            };
            return loanCalculationResult;
        }
    }
}