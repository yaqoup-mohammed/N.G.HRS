using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class jh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAdvances_Currency_CurrencyId",
                table: "EmployeeAdvances");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAdvances_Currency_CurrencyId",
                table: "EmployeeAdvances",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAdvances_Currency_CurrencyId",
                table: "EmployeeAdvances");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAdvances_Currency_CurrencyId",
                table: "EmployeeAdvances",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
