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
                name: "WeekendsId",
                table: "permanenceModels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WeekendsId",
                table: "periods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_permanenceModels_WeekendsId",
                table: "permanenceModels",
                column: "WeekendsId");

            migrationBuilder.CreateIndex(
                name: "IX_periods_WeekendsId",
                table: "periods",
                column: "WeekendsId");

            migrationBuilder.AddForeignKey(
                name: "FK_periods_weekends_WeekendsId",
                table: "periods",
                column: "WeekendsId",
                principalTable: "weekends",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_permanenceModels_weekends_WeekendsId",
                table: "permanenceModels",
                column: "WeekendsId",
                principalTable: "weekends",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_periods_weekends_WeekendsId",
                table: "periods");

            migrationBuilder.DropForeignKey(
                name: "FK_permanenceModels_weekends_WeekendsId",
                table: "permanenceModels");

            migrationBuilder.DropIndex(
                name: "IX_permanenceModels_WeekendsId",
                table: "permanenceModels");

            migrationBuilder.DropIndex(
                name: "IX_periods_WeekendsId",
                table: "periods");

            migrationBuilder.DropColumn(
                name: "WeekendsId",
                table: "permanenceModels");

            migrationBuilder.DropColumn(
                name: "WeekendsId",
                table: "periods");
        }
    }
}
