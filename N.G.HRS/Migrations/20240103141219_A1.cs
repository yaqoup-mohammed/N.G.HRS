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
                name: "OneFingerprintId",
                table: "employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_employee_OneFingerprintId",
                table: "employee",
                column: "OneFingerprintId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_oneFingerprints_OneFingerprintId",
                table: "employee",
                column: "OneFingerprintId",
                principalTable: "oneFingerprints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_oneFingerprints_OneFingerprintId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_OneFingerprintId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "OneFingerprintId",
                table: "employee");
        }
    }
}
