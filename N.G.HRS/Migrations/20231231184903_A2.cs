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
            migrationBuilder.DropForeignKey(
                name: "FK_maritalStatuses_personalDatas_PersonalDataId",
                table: "maritalStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_maritalStatuses_personalDatas_PersonalDataId",
                table: "maritalStatuses",
                column: "PersonalDataId",
                principalTable: "personalDatas",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_maritalStatuses_personalDatas_PersonalDataId",
                table: "maritalStatuses");

            migrationBuilder.AddForeignKey(
                name: "FK_maritalStatuses_personalDatas_PersonalDataId",
                table: "maritalStatuses",
                column: "PersonalDataId",
                principalTable: "personalDatas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
