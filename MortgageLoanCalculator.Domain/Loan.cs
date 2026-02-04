using System.ComponentModel.DataAnnotations;

namespace MortgageLoanCalculator.Domain;

/// <summary>
/// Represents a mortgage loan application with borrower information, property details, and loan terms.
/// </summary>
public class Loan
{
    public int LoanId { get; set;}

    [Required(ErrorMessage = "Annual income is required")]
    [Range(MortgageCalculatorConstants.MinAnnualIncome, MortgageCalculatorConstants.MaxAnnualIncome,
                     ErrorMessage = "Annual Income must be between {1:C} and {2:C}")]
    public double? AnnualIncome { get; set; }

    // Used to calculate equity percentage and determine if loan insurance is required
    [Required(ErrorMessage = "Market value is required")]
    [Range(MortgageCalculatorConstants.MinPropertyPrice, MortgageCalculatorConstants.MaxPropertyPrice,
            ErrorMessage = "Market value must be between {1:C} and {2:C}")]
    public double? MarketValue { get; set; }

    [Required(ErrorMessage = "Purchase price is required")]
    [Range(MortgageCalculatorConstants.MinPropertyPrice, MortgageCalculatorConstants.MaxPropertyPrice,
            ErrorMessage = "Purchase price must be between {1:C} and {2:C}")]
    public double? PurchasePrice { get; set; }

    [Required(ErrorMessage = "Down payment is required")]
    [Range(MortgageCalculatorConstants.MinDownPayment, MortgageCalculatorConstants.MaxDownPayment,
            ErrorMessage = "Down payment must be between {1:C} and {2:C}")]
    public double? DownPayment { get; set; }

    // As a percentage (e.g., 5.5 represents 5.5%)
    [Required(ErrorMessage = "Annual interest rate is required")]
    [Range(MortgageCalculatorConstants.MinInterestRate, MortgageCalculatorConstants.MaxInterestRate,
            ErrorMessage = "Annual interest rate must be between {1}% and {2}%")]
    public double? AnnualInterestRate { get; set; }

    // Common values: 12 (monthly), 24 (semi-monthly), or 4 (quarterly)
    [Required(ErrorMessage = "Number of payments per year is required")]
    [Range(1, 24, ErrorMessage = "Number of payments per year must be between {1} and {2}")]
    public PaymentsPerYear NumPaymentsPerYear { get; set; }

    [Required(ErrorMessage = "Loan term is required")]
    public LoanTerm Term { get; set; }

    // Set to 0 if the property has no HOA
    [Required(ErrorMessage = "Annual HOA fee is required")]
    [Range(0, MortgageCalculatorConstants.MaxAnnualHoaFee,
            ErrorMessage = "Annual HOA fee must be between {1:C} and {2:C}")]
    public double? AnnualHoaFee { get; set; }

    public Loan()
    {
        
    }

    public Loan(int loanId, double annualIncome, double marketValue, double purchasePrice,
                double downPayment, double annualInterestRate, PaymentsPerYear numPaymentsPerYear, LoanTerm term, double annualHoaFee)
    {
        LoanId = loanId;
        AnnualIncome = annualIncome;
        MarketValue = marketValue;
        PurchasePrice = purchasePrice;
        DownPayment = downPayment;
        AnnualInterestRate = annualInterestRate;
        NumPaymentsPerYear = numPaymentsPerYear;
        Term = term;
        AnnualHoaFee = annualHoaFee;
    }
}