using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                table: "allowancesAndDiscounts");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "allowancesAndDiscounts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                table: "allowancesAndDiscounts",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                table: "allowancesAndDiscounts");

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "allowancesAndDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                table: "allowancesAndDiscounts",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
