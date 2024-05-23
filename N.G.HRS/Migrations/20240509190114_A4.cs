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
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecord_Sections_SectionsId",
                table: "AttendanceRecord");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceRecord_SectionsId",
                table: "AttendanceRecord");

            migrationBuilder.DropColumn(
                name: "SectionsId",
                table: "AttendanceRecord");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_SectionId",
                table: "AttendanceRecord",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecord_Sections_SectionId",
                table: "AttendanceRecord",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AttendanceRecord_Sections_SectionId",
                table: "AttendanceRecord");

            migrationBuilder.DropIndex(
                name: "IX_AttendanceRecord_SectionId",
                table: "AttendanceRecord");

            migrationBuilder.AddColumn<int>(
                name: "SectionsId",
                table: "AttendanceRecord",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_SectionsId",
                table: "AttendanceRecord",
                column: "SectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AttendanceRecord_Sections_SectionsId",
                table: "AttendanceRecord",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id");
        }
    }
}
