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
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "fingerprintDevices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_fingerprintDevices_EmployeeId",
                table: "fingerprintDevices",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_fingerprintDevices_employee_EmployeeId",
                table: "fingerprintDevices",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_fingerprintDevices_employee_EmployeeId",
                table: "fingerprintDevices");

            migrationBuilder.DropIndex(
                name: "IX_fingerprintDevices_EmployeeId",
                table: "fingerprintDevices");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "fingerprintDevices");
        }
    }
}
