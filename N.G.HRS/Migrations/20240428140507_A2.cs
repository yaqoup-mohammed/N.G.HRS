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
            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_Periods_PeriodsId",
                table: "staffTimes");

            migrationBuilder.DropIndex(
                name: "IX_staffTimes_PeriodsId",
                table: "staffTimes");

            migrationBuilder.DropColumn(
                name: "PeriodsId",
                table: "staffTimes");

            migrationBuilder.CreateIndex(
                name: "IX_staffTimes_PeriodId",
                table: "staffTimes",
                column: "PeriodId");

            migrationBuilder.AddForeignKey(
                name: "FK_staffTimes_Periods_PeriodId",
                table: "staffTimes",
                column: "PeriodId",
                principalTable: "Periods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_Periods_PeriodId",
                table: "staffTimes");

            migrationBuilder.DropIndex(
                name: "IX_staffTimes_PeriodId",
                table: "staffTimes");

            migrationBuilder.AddColumn<int>(
                name: "PeriodsId",
                table: "staffTimes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_staffTimes_PeriodsId",
                table: "staffTimes",
                column: "PeriodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_staffTimes_Periods_PeriodsId",
                table: "staffTimes",
                column: "PeriodsId",
                principalTable: "Periods",
                principalColumn: "Id");
        }
    }
}
