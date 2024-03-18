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
            migrationBuilder.DropForeignKey(
                name: "FK_weekends_permanenceModels_PermanenceModelsId1",
                table: "weekends");

            migrationBuilder.DropIndex(
                name: "IX_weekends_PermanenceModelsId1",
                table: "weekends");

            migrationBuilder.DropColumn(
                name: "PermanenceModelsId1",
                table: "weekends");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PermanenceModelsId1",
                table: "weekends",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_weekends_PermanenceModelsId1",
                table: "weekends",
                column: "PermanenceModelsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_weekends_permanenceModels_PermanenceModelsId1",
                table: "weekends",
                column: "PermanenceModelsId1",
                principalTable: "permanenceModels",
                principalColumn: "Id");
        }
    }
}
