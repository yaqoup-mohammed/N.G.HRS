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
                name: "FunctionalClassId",
                table: "Currency",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Currency_FunctionalClassId",
                table: "Currency",
                column: "FunctionalClassId");

            migrationBuilder.AddForeignKey(
                name: "FK_Currency_functionalClasses_FunctionalClassId",
                table: "Currency",
                column: "FunctionalClassId",
                principalTable: "functionalClasses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Currency_functionalClasses_FunctionalClassId",
                table: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_Currency_FunctionalClassId",
                table: "Currency");

            migrationBuilder.DropColumn(
                name: "FunctionalClassId",
                table: "Currency");
        }
    }
}
