namespace MortgageLoanCalculator.Domain
{
    /// <summary>
    /// Provides mortgage loan calculation services including payment schedules, fees, and equity computations.
    /// All calculations assume that origination fees and closing costs are financed into the loan principal.
    /// 
    /// - Domain models (Loan) use enums to represent user intent
    /// - Calculator converts enums to numeric values at a single boundary
    /// - All math operates strictly on numeric types (int/double)
    /// - HOA fees are provided annually and divided by payments per year
    /// 
    /// </summary>
    public class LoanCalculator
    {
        // Helper methods to convert enum values to integers
        private int PaymentsPerYearValue(PaymentsPerYear paymentsPerYear)
        {
            return (int)paymentsPerYear;
        }

        private int LoanTermYears(LoanTerm term)
        {
            return (int)term;
        }
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
            int paymentsPerYear = PaymentsPerYearValue(loan.NumPaymentsPerYear);
            int termYears = LoanTermYears(loan.Term);

            double monthlyRate = CalculateMonthlyInterestRate(loan.AnnualInterestRate.Value, paymentsPerYear);
            int totalPayments = CalculateTotalPaymentsPerLoan(paymentsPerYear, termYears);

            double growthFactor = Math.Pow(1 + monthlyRate, totalPayments);

            return CalculateLoanAmount(loan.PurchasePrice.Value, loan.DownPayment.Value) * (monthlyRate * growthFactor) / (growthFactor - 1);
        }

        // Calculates total periodic payment including all fees.
        private double CalculateMonthlyPaymentTotalWithFees(Loan loan)
        {
            int paymentsPerYear = PaymentsPerYearValue(loan.NumPaymentsPerYear);

            return CalculateMonthlyPaymentPI(loan) + CalculateLoanInsurancePerMonth(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value, paymentsPerYear) 
                    + (loan.AnnualHoaFee.Value / paymentsPerYear) + CalculatePropertyTaxPerMonth(loan.MarketValue.Value, paymentsPerYear) 
                    + CalculateHomeownersInsurancePerMonth(loan.MarketValue.Value, paymentsPerYear);
        }

        public LoanCalculationResult CalculateResult(Loan loan)
        {
            if (loan == null)
            {
                throw new ArgumentNullException(nameof(loan));
            }

            int paymentsPerYear = PaymentsPerYearValue(loan.NumPaymentsPerYear);
            int termYears = LoanTermYears(loan.Term);

            LoanCalculationResult loanCalculationResult = new LoanCalculationResult()
            {
                LoanAmount = CalculateLoanAmount(loan.PurchasePrice.Value, loan.DownPayment.Value),
                OriginationFee = CalculateOriginationFee(loan.PurchasePrice.Value, loan.DownPayment.Value),
                ClosingCosts = MortgageCalculatorConstants.ClosingCosts,
                TotalPaymentsPerLoan = CalculateTotalPaymentsPerLoan(paymentsPerYear, termYears),
                EquityPercentage = CalculateEquityPercentage(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value),
                EquityValue = CalculateEquityValue(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value),
                LoanInsurancePerMonth = CalculateLoanInsurancePerMonth(loan.MarketValue.Value, loan.PurchasePrice.Value, loan.DownPayment.Value, paymentsPerYear),
                PropertyTaxPerMonth = CalculatePropertyTaxPerMonth(loan.MarketValue.Value, paymentsPerYear),
                HomeownersInsurancePerMonth = CalculateHomeownersInsurancePerMonth(loan.MarketValue.Value, paymentsPerYear),
                MonthlyPaymentPI = CalculateMonthlyPaymentPI(loan),
                MonthlyPaymentTotalWithFees = CalculateMonthlyPaymentTotalWithFees(loan),
            };
            return loanCalculationResult;
        }
    }
}