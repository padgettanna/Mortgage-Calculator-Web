using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MortgageLoanCalculator.Data;
using MortgageLoanCalculator.Domain;
using MortgageLoanCalculator.Models;
using System.Security.Claims;
using System.Text.Json;

namespace MortgageLoanCalculator.Controllers
{
    /// <summary>
    /// Handles mortgage loan creation, calculation, and result display.
    /// Persists user-specific mortgage calculation snapshots for later comparison.
    /// 
    /// - Accept user input via Loan model
    /// - Invoke domain services to calculate mortgage results
    /// - Persist user-specific calculation snapshots
    /// - Support "Adjust Values" by restoring saved inputs
    /// - Authentication is required for all actions
    /// </summary>
    [Authorize]
    public class LoanController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LoanController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: Display the loan input form
        public IActionResult Create()
        {
            return View();
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
            int paymentsPerYear = (int)loan.NumPaymentsPerYear;

            // Build result view model
            MortgageResultViewModel resultViewModel = new MortgageResultViewModel
            {
                AnnualIncome = loan.AnnualIncome.Value,
                PurchasePrice = loan.PurchasePrice.Value,
                DownPayment = loan.DownPayment.Value,
                AnnualInterestRate = loan.AnnualInterestRate.Value,
                MonthlyHoaFee = loan.AnnualHoaFee.Value / paymentsPerYear,
                MonthlyPaymentIncomeRatio = paymentIncomeRatio,
                Term = loan.Term,
                Calculation = calculation,
                Decision = decision
            };

            var snapshot = new MortgageCalculationSnapshot
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),

                AnnualIncome = loan.AnnualIncome.Value,
                MarketValue = loan.MarketValue.Value,
                PurchasePrice = loan.PurchasePrice.Value,
                DownPayment = loan.DownPayment.Value,
                AnnualInterestRate = loan.AnnualInterestRate.Value,
                MonthlyHoaFee = loan.AnnualHoaFee.Value / paymentsPerYear,
                NumPaymentsPerYear = loan.NumPaymentsPerYear,
                Term = loan.Term,

                LoanAmount = calculation.LoanAmount,
                MonthlyPayment = calculation.MonthlyPaymentPI,
                TotalMonthlyPayment = calculation.MonthlyPaymentTotalWithFees,
                PaymentIncomeRatio = paymentIncomeRatio,
                Decision = decision.Decision
            };

            _context.MortgageCalculations.Add(snapshot);
            _context.SaveChanges();

            // Expose snapshot ID for Adjust functionality
            resultViewModel.SnapshotId = snapshot.Id;
            return View("Result", resultViewModel);
        }

        // Clears the current workflow and returns a blank form.
        public IActionResult Reset()
        {
            return RedirectToAction("Create");
        }

        // Restores a previously saved snapshot and pre-fills the input form to allow the user to adjust values.
        public IActionResult Adjust(int id)
        {
            var snapshot = _context.MortgageCalculations
                .FirstOrDefault(s => s.Id == id
                    && s.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (snapshot == null)
            {
                return NotFound();
            }

            // Map snapshot inputs back to Loan model
            var loan = new Loan
            {
                AnnualIncome = snapshot.AnnualIncome,
                MarketValue = snapshot.MarketValue,
                PurchasePrice = snapshot.PurchasePrice,
                DownPayment = snapshot.DownPayment,
                AnnualInterestRate = snapshot.AnnualInterestRate,
                AnnualHoaFee = snapshot.MonthlyHoaFee * 12,
                Term = snapshot.Term,
                NumPaymentsPerYear = snapshot.NumPaymentsPerYear
            };

            return View("Create", loan);
        }
        public IActionResult History()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var snapshots = _context.MortgageCalculations
                .Where(s => s.UserId == userId)
                .OrderByDescending(s => s.CreatedAt)
                .Select(s => new MortgageCalculationListItemViewModel
                {
                    SnapshotId = s.Id,
                    PurchasePrice = s.PurchasePrice,
                    DownPayment = s.DownPayment,
                    AnnualInterestRate = s.AnnualInterestRate,
                    Term = s.Term,
                    TotalMonthlyPayment = s.TotalMonthlyPayment,
                    PaymentIncomeRatio = s.PaymentIncomeRatio,
                    Decision = s.Decision,
                    CreatedAt = s.CreatedAt
                })
                .ToList();

            return View(snapshots);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var snapshot = _context.MortgageCalculations
                .FirstOrDefault(s => s.Id == id &&
                    s.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (snapshot == null)
            {
                return NotFound();
            }

            _context.MortgageCalculations.Remove(snapshot);
            _context.SaveChanges();

            return RedirectToAction("History");
        }

    }
}
