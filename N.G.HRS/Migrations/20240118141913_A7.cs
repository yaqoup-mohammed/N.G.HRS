using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_financialStatements_FinancialStatementsId",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Currency_FinancialStatementsId",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "FinancialStatementsId",
                table: "Currency");

            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "financialStatements",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_financialStatements_CurrencyId",
                table: "financialStatements",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_financialStatements_Currency_CurrencyId",
                table: "financialStatements",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financialStatements_Currency_CurrencyId",
                table: "financialStatements");

            migrationBuilder.DropIndex(
                name: "IX_financialStatements_CurrencyId",
                table: "financialStatements");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "financialStatements");

            migrationBuilder.AddColumn<int>(
                name: "FinancialStatementsId",
                table: "Currency",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_FinancialStatementsId",
                table: "Currency",
                column: "FinancialStatementsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_financialStatements_FinancialStatementsId",
                table: "Currency",
                column: "FinancialStatementsId",
                principalTable: "financialStatements",
                principalColumn: "Id");
        }
    }
}
