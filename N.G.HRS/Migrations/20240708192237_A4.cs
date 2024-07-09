using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Currency_CurrencyId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "CurrentJop",
                table: "employee");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Salaries",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "EarlyLeave",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "RetirementInsurance",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Currency_CurrencyId",
                table: "Salaries",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Salaries_Currency_CurrencyId",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "EarlyLeave",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "RetirementInsurance",
                table: "Salaries");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "Salaries",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "CurrentJop",
                table: "employee",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Salaries_Currency_CurrencyId",
                table: "Salaries",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }
    }
}
