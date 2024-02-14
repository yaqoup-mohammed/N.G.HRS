using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class New2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financialStatements_Currency_CurrencyId",
                table: "financialStatements");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "financialStatements",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_financialStatements_Currency_CurrencyId",
                table: "financialStatements",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_financialStatements_Currency_CurrencyId",
                table: "financialStatements");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "financialStatements",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_financialStatements_Currency_CurrencyId",
                table: "financialStatements",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }
    }
}
