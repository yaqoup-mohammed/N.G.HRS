using System;
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
            migrationBuilder.CreateTable(
                name: "EndOfServiceClearance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReasonForClearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastApprovedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServicePeriodPerYear = table.Column<int>(type: "int", nullable: false),
                    EndOfServiceBenefits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancesAndLoans = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VacationEntitlements = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Absence = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherEntitlements = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfServiceClearance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndOfServiceClearance_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EndOfServiceClearance_EmployeeId",
                table: "EndOfServiceClearance",
                column: "EmployeeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EndOfServiceClearance");
        }
    }
}
