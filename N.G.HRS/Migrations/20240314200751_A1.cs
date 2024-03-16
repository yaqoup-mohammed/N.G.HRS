using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PeriodId",
                table: "staffTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodsId",
                table: "staffTimes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PeriodsName",
                table: "Periods",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_Periods_PeriodsId",
                table: "staffTimes");

            migrationBuilder.DropIndex(
                name: "IX_staffTimes_PeriodsId",
                table: "staffTimes");

            migrationBuilder.DropColumn(
                name: "PeriodId",
                table: "staffTimes");

            migrationBuilder.DropColumn(
                name: "PeriodsId",
                table: "staffTimes");

            migrationBuilder.AlterColumn<string>(
                name: "PeriodsName",
                table: "Periods",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }
    }
}
