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
            migrationBuilder.AddColumn<int>(
                name: "OpeningBalancesForVacationsId",
                table: "publicHolidays",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OpeningBalancesForVacationsId",
                table: "employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_publicHolidays_OpeningBalancesForVacationsId",
                table: "publicHolidays",
                column: "OpeningBalancesForVacationsId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_OpeningBalancesForVacationsId",
                table: "employee",
                column: "OpeningBalancesForVacationsId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_openingBalancesForVacations_OpeningBalancesForVacationsId",
                table: "employee",
                column: "OpeningBalancesForVacationsId",
                principalTable: "openingBalancesForVacations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_publicHolidays_openingBalancesForVacations_OpeningBalancesForVacationsId",
                table: "publicHolidays",
                column: "OpeningBalancesForVacationsId",
                principalTable: "openingBalancesForVacations",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_openingBalancesForVacations_OpeningBalancesForVacationsId",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_publicHolidays_openingBalancesForVacations_OpeningBalancesForVacationsId",
                table: "publicHolidays");

            migrationBuilder.DropIndex(
                name: "IX_publicHolidays_OpeningBalancesForVacationsId",
                table: "publicHolidays");

            migrationBuilder.DropIndex(
                name: "IX_employee_OpeningBalancesForVacationsId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "OpeningBalancesForVacationsId",
                table: "publicHolidays");

            migrationBuilder.DropColumn(
                name: "OpeningBalancesForVacationsId",
                table: "employee");
        }
    }
}
