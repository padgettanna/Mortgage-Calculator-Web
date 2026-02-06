using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MortgageLoanCalculator.Models;

namespace MortgageLoanCalculator.Controllers
{
    /// <summary>
    /// Provides public-facing pages that do not require authentication.
    /// Includes landing, privacy, and error handling views.
    /// </summary>
    [AllowAnonymous]
    public class HomeController : Controller
    {
        // Landing page for the application
        public IActionResult Index()
        {
            return View();
        }

        // Static privacy policy page
        public IActionResult Privacy()
        {
            return View();
        }

        // Displays unhandled error information in a user-friendly format
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
