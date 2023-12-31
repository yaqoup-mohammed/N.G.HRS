using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualificationId",
                table: "specialties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_specialties_QualificationId",
                table: "specialties",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_specialties_qualifications_QualificationId",
                table: "specialties",
                column: "QualificationId",
                principalTable: "qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_specialties_qualifications_QualificationId",
                table: "specialties");

            migrationBuilder.DropIndex(
                name: "IX_specialties_QualificationId",
                table: "specialties");

            migrationBuilder.DropColumn(
                name: "QualificationId",
                table: "specialties");
        }
    }
}
