using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MortgageLoanCalculator.Migrations
{
    /// <inheritdoc />
    public partial class AddMArketValueToSnapshot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "MarketValue",
                table: "MortgageCalculations",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketValue",
                table: "MortgageCalculations");
        }
    }
}
