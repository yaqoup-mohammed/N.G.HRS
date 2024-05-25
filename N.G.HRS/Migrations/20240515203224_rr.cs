using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class rr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationAllowances_employee_EmployeeId",
                table: "VacationAllowances");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "VacationAllowances",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_VacationAllowances_employee_EmployeeId",
                table: "VacationAllowances",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VacationAllowances_employee_EmployeeId",
                table: "VacationAllowances");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "VacationAllowances",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_VacationAllowances_employee_EmployeeId",
                table: "VacationAllowances",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
