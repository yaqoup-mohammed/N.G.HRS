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
            migrationBuilder.AddColumn<int>(
                name: "SectorsId",
                table: "branches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_branches_SectorsId",
                table: "branches",
                column: "SectorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_branches_sectors_SectorsId",
                table: "branches",
                column: "SectorsId",
                principalTable: "sectors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_branches_sectors_SectorsId",
                table: "branches");

            migrationBuilder.DropIndex(
                name: "IX_branches_SectorsId",
                table: "branches");

            migrationBuilder.DropColumn(
                name: "SectorsId",
                table: "branches");
        }
    }
}
