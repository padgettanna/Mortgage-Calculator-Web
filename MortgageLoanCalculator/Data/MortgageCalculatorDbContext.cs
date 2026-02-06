using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MortgageLoanCalculator.Models;

namespace MortgageLoanCalculator.Data
{
    /// <summary>
    /// Application database context that integrates ASP.NET Identity
    /// with domain-specific application data.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MortgageCalculationSnapshot> MortgageCalculations { get; set; }
    }
}
