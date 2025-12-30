using Microsoft.AspNetCore.Mvc;
using MortgageLoanCalculator.Domain;
using MortgageLoanCalculator.Models;
using System.Text.Json;

namespace MortgageLoanCalculator.Controllers
{
    public class LoanController : Controller
    {
        public IActionResult Create()
        {
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

        [HttpPost]
        public IActionResult Create(Loan loan)
        {
            if (!ModelState.IsValid)
            {
                return View(loan);
            }

            var loanCalculator = new LoanCalculator();
            var calculation = loanCalculator.CalculateResult(loan);

            var decisionService = new LoanDecisionService();
            var decision = decisionService.DetermineEligibility(calculation.MonthlyPaymentTotalWithFees, loan.AnnualIncome.Value);
            double paymentIncomeRatio = (calculation.MonthlyPaymentTotalWithFees / (loan.AnnualIncome.Value / 12)) * 100;


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
            TempData["Loan"] = JsonSerializer.Serialize(loan);
            return View("Result", resultViewModel);
        }

        public IActionResult Reset()
        {
            TempData.Clear();
            return RedirectToAction("Create");
        }
    }
}
