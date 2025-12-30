using System.ComponentModel.DataAnnotations;

namespace MortgageLoanCalculator.Domain;

public class Loan
{
    public int LoanId { get; set;}

    [Required(ErrorMessage = "First name is required")]
    [StringLength(32, ErrorMessage = "First name can not exceed 32 characters")]
    public string BorrowerFirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(32, ErrorMessage = "Last name can not exceed 32 characters")]
    public string BorrowerLastName { get; set;}

    [Required(ErrorMessage = "Annual income is required")]
    [Range(MortgageCalculatorConstants.MinAnnualIncome, MortgageCalculatorConstants.MaxAnnualIncome,
                     ErrorMessage = "Annual Income must be between {1:C} and {2:C}")]
    public double? AnnualIncome { get; set; }

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

    [Required(ErrorMessage = "Annual interest rate is required")]
    [Range(MortgageCalculatorConstants.MinInterestRate, MortgageCalculatorConstants.MaxInterestRate,
            ErrorMessage = "Annual interest rate must be between {1}% and {2}%")]
    public double? AnnualInterestRate { get; set; }

    [Required(ErrorMessage = "Number of payments per year is required")]
    [Range(1, 24, ErrorMessage = "Number of payments per year must be between {1} and {2}")]
    public int NumPaymentsPerYear { get; set; }

    [Required(ErrorMessage = "Loan term is required")]
    public LoanTerm Term { get; set; }

    [Required(ErrorMessage = "Annual HOA fee is required")]
    [Range(0, MortgageCalculatorConstants.MaxAnnualHoaFee,
            ErrorMessage = "Annual HOA fee must be between {1:C} and {2:C}")]
    public double? AnnualHoaFee { get; set; }

    public Loan()
    {
        
    }

    public Loan(int loanId, string borrowerFirstName, string borrowerLastName, double annualIncome, double marketValue, double purchasePrice,
                double downPayment, double annualInterestRate, int numPaymentsPerYear, LoanTerm term, double annualHoaFee)
    {
        LoanId = loanId;
        BorrowerFirstName = borrowerFirstName;
        BorrowerLastName = borrowerLastName;
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