using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QualificationId",
                table: "educationalQualifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_educationalQualifications_QualificationId",
                table: "educationalQualifications",
                column: "QualificationId");

            migrationBuilder.AddForeignKey(
                name: "FK_educationalQualifications_qualifications_QualificationId",
                table: "educationalQualifications",
                column: "QualificationId",
                principalTable: "qualifications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_educationalQualifications_qualifications_QualificationId",
                table: "educationalQualifications");

            migrationBuilder.DropIndex(
                name: "IX_educationalQualifications_QualificationId",
                table: "educationalQualifications");

            migrationBuilder.DropColumn(
                name: "QualificationId",
                table: "educationalQualifications");
        }
    }
}
