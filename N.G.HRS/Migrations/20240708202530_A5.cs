using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Salaries",
                newName: "WorkedHours");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseSalary",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "HalfAbcents",
                table: "Salaries",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "ToTime",
                table: "AttendanceRecord",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseSalary",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "HalfAbcents",
                table: "Salaries");

            migrationBuilder.DropColumn(
                name: "ToTime",
                table: "AttendanceRecord");

            migrationBuilder.RenameColumn(
                name: "WorkedHours",
                table: "Salaries",
                newName: "Salary");
        }
    }
}
