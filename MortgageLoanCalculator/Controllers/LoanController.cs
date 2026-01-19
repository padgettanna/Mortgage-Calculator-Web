using Microsoft.AspNetCore.Mvc;
using MortgageLoanCalculator.Domain;
using MortgageLoanCalculator.Models;
using System.Text.Json;

namespace MortgageLoanCalculator.Controllers
{
    /// <summary>
    /// Handles mortgage loan creation, calculation, and result display.
    /// Uses TempData to preserve loan information between requests.
    /// </summary>
    public class LoanController : Controller
    {
        // GET: Display the loan input form
        public IActionResult Create()
        {
            // Restore loan data from TempData if available (e.g., after viewing results)
            var loanJson = TempData["Loan"] as string;
            
            if (loanJson != null)
            {
                var savedLoan = JsonSerializer.Deserialize<Loan>(loanJson);
                return View(savedLoan);
            }
            else
            {
                return View();
            }
        }

        // POST: Process loan application and calculate results
        [HttpPost]
        public IActionResult Create(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return View(loan);
            }

            // Calculate loan metrics
            var loanCalculator = new LoanCalculator();
            var calculation = loanCalculator.CalculateResult(loan);

            // Determine approval/denial based on payment-to-income ratio
            var decisionService = new LoanDecisionService();
            var decision = decisionService.DetermineEligibility(calculation.MonthlyPaymentTotalWithFees, loan.AnnualIncome.Value);
            double paymentIncomeRatio = (calculation.MonthlyPaymentTotalWithFees / (loan.AnnualIncome.Value / 12)) * 100;

            // Build result view model
            MortgageResultViewModel resultViewModel = new MortgageResultViewModel
            {
                BorrowerName = $"{loan.BorrowerFirstName} {loan.BorrowerLastName}",
                AnnualIncome = loan.AnnualIncome.Value,
                PurchasePrice = loan.PurchasePrice.Value,
                DownPayment = loan.DownPayment.Value,
                AnnualInterestRate = loan.AnnualInterestRate.Value,
                MonthlyHoaFee = loan.AnnualHoaFee.Value / loan.NumPaymentsPerYear,
                MonthlyPaymentIncomeRatio = paymentIncomeRatio,
                Term = loan.Term,
                Calculation = calculation,
                Decision = decision
            };

            // Save loan to TempData for "Adjust Values" feature
            TempData["Loan"] = JsonSerializer.Serialize(loan);
            return View("Result", resultViewModel);
        }

        // Clear TempData and start fresh
        public IActionResult Reset()
        {
            TempData.Clear();
            return RedirectToAction("Create");
        }
    }
}
