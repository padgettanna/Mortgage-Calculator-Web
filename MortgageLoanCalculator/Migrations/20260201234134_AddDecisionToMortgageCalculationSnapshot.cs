using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MortgageLoanCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddDecisionToMortgageCalculationSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Decision",
                table: "MortgageCalculations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Decision",
                table: "MortgageCalculations");
        }
    }
}
