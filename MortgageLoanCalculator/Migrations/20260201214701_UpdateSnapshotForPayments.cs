using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MortgageLoanCalculator.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSnapshotForPayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumPaymentsPerYear",
                table: "MortgageCalculations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumPaymentsPerYear",
                table: "MortgageCalculations");
        }
    }
}
