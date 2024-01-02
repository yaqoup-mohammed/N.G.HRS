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
            migrationBuilder.DropTable(
                name: "archives");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeAccountId",
                table: "employee",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "EmployeeAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAccount", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_employee_EmployeeAccountId",
                table: "employee",
                column: "EmployeeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_EmployeeAccount_EmployeeAccountId",
                table: "employee",
                column: "EmployeeAccountId",
                principalTable: "EmployeeAccount",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_employee_EmployeeAccount_EmployeeAccountId",
                table: "employee");

            migrationBuilder.DropTable(
                name: "EmployeeAccount");

            migrationBuilder.DropIndex(
                name: "IX_employee_EmployeeAccountId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "EmployeeAccountId",
                table: "employee");

            migrationBuilder.CreateTable(
                name: "archives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archives", x => x.Id);
                });
        }
    }
}
