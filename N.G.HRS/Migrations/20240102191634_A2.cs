using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentsId",
                table: "sectors",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_sectors_DepartmentsId",
                table: "sectors",
                column: "DepartmentsId");

            migrationBuilder.AddForeignKey(
                name: "FK_sectors_Departments_DepartmentsId",
                table: "sectors",
                column: "DepartmentsId",
                principalTable: "Departments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_sectors_Departments_DepartmentsId",
                table: "sectors");

            migrationBuilder.DropIndex(
                name: "IX_sectors_DepartmentsId",
                table: "sectors");

            migrationBuilder.DropColumn(
                name: "DepartmentsId",
                table: "sectors");
        }
    }
}
