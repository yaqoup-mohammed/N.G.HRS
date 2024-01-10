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
                name: "EmployeesQualifications",
                columns: table => new
                {
                    employeesId = table.Column<int>(type: "int", nullable: false),
                    qualificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesQualifications", x => new { x.employeesId, x.qualificationsId });
                    table.ForeignKey(
                        name: "FK_EmployeesQualifications_employee_employeesId",
                        column: x => x.employeesId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesQualifications_qualifications_qualificationsId",
                        column: x => x.qualificationsId,
                        principalTable: "qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesQualifications_qualificationsId",
                table: "EmployeesQualifications",
                column: "qualificationsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeesQualifications");
        }
    }
}
