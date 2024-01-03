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
            migrationBuilder.CreateTable(
                name: "penaltiesAndViolationsForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NumberOfTime = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_penaltiesAndViolationsForms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PenaltiesName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Deduction = table.Column<bool>(type: "bit", nullable: false),
                    DiscountFromWorkingHours = table.Column<bool>(type: "bit", nullable: false),
                    DeductionFromTheDailyWage = table.Column<bool>(type: "bit", nullable: false),
                    DeductionFromSalary = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: true),
                    Percent = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PenaltiesAndViolationsFormsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Penalties_penaltiesAndViolationsForms_PenaltiesAndViolationsFormsId",
                        column: x => x.PenaltiesAndViolationsFormsId,
                        principalTable: "penaltiesAndViolationsForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolationsName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PenaltiesAndViolationsFormsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Violations_penaltiesAndViolationsForms_PenaltiesAndViolationsFormsId",
                        column: x => x.PenaltiesAndViolationsFormsId,
                        principalTable: "penaltiesAndViolationsForms",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_PenaltiesAndViolationsFormsId",
                table: "Penalties",
                column: "PenaltiesAndViolationsFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_PenaltiesAndViolationsFormsId",
                table: "Violations",
                column: "PenaltiesAndViolationsFormsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "Violations");

            migrationBuilder.DropTable(
                name: "penaltiesAndViolationsForms");
        }
    }
}
