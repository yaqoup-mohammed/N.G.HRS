using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobDescriptionId",
                table: "jobRanks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobDescriptionId",
                table: "functionalClasses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "JobDescriptionId",
                table: "functionalCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_jobRanks_JobDescriptionId",
                table: "jobRanks",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_functionalClasses_JobDescriptionId",
                table: "functionalClasses",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_functionalCategories_JobDescriptionId",
                table: "functionalCategories",
                column: "JobDescriptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_functionalCategories_jobDescriptions_JobDescriptionId",
                table: "functionalCategories",
                column: "JobDescriptionId",
                principalTable: "jobDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_functionalClasses_jobDescriptions_JobDescriptionId",
                table: "functionalClasses",
                column: "JobDescriptionId",
                principalTable: "jobDescriptions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_jobRanks_jobDescriptions_JobDescriptionId",
                table: "jobRanks",
                column: "JobDescriptionId",
                principalTable: "jobDescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_functionalCategories_jobDescriptions_JobDescriptionId",
                table: "functionalCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_functionalClasses_jobDescriptions_JobDescriptionId",
                table: "functionalClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_jobRanks_jobDescriptions_JobDescriptionId",
                table: "jobRanks");

            migrationBuilder.DropIndex(
                name: "IX_jobRanks_JobDescriptionId",
                table: "jobRanks");

            migrationBuilder.DropIndex(
                name: "IX_functionalClasses_JobDescriptionId",
                table: "functionalClasses");

            migrationBuilder.DropIndex(
                name: "IX_functionalCategories_JobDescriptionId",
                table: "functionalCategories");

            migrationBuilder.DropColumn(
                name: "JobDescriptionId",
                table: "jobRanks");

            migrationBuilder.DropColumn(
                name: "JobDescriptionId",
                table: "functionalClasses");

            migrationBuilder.DropColumn(
                name: "JobDescriptionId",
                table: "functionalCategories");
        }
    }
}
