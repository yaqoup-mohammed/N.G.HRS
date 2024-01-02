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
                name: "SectionsId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SectionsId",
                table: "Departments",
                column: "SectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Sections_SectionsId",
                table: "Departments",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Sections_SectionsId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_SectionsId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "SectionsId",
                table: "Departments");
        }
    }
}
