using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "allowancesAndDiscounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_allowancesAndDiscounts_CurrencyId",
                table: "allowancesAndDiscounts",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                table: "allowancesAndDiscounts",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                table: "allowancesAndDiscounts");

            migrationBuilder.DropIndex(
                name: "IX_allowancesAndDiscounts_CurrencyId",
                table: "allowancesAndDiscounts");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "allowancesAndDiscounts");
        }
    }
}
