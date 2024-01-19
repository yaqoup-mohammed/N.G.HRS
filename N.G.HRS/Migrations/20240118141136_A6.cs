using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_functionalCategories_Currency_CurrencyId",
                table: "functionalCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobDescription_functionalCategories_FunctionalCategoriesId",
                table: "JobDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_JobDescription_jobRanks_JobRanksId",
                table: "JobDescription");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "jobRanks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "JobDescription",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "JobRanksId",
                table: "JobDescription",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "FunctionalCategoriesId",
                table: "JobDescription",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "functionalClasses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "functionalCategories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "functionalCategories",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_functionalCategories_Currency_CurrencyId",
                table: "functionalCategories",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobDescription_functionalCategories_FunctionalCategoriesId",
                table: "JobDescription",
                column: "FunctionalCategoriesId",
                principalTable: "functionalCategories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobDescription_jobRanks_JobRanksId",
                table: "JobDescription",
                column: "JobRanksId",
                principalTable: "jobRanks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_functionalCategories_Currency_CurrencyId",
                table: "functionalCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_JobDescription_functionalCategories_FunctionalCategoriesId",
                table: "JobDescription");

            migrationBuilder.DropForeignKey(
                name: "FK_JobDescription_jobRanks_JobRanksId",
                table: "JobDescription");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "jobRanks",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "JobDescription",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "JobRanksId",
                table: "JobDescription",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "FunctionalCategoriesId",
                table: "JobDescription",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "functionalClasses",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "functionalCategories",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "functionalCategories",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_functionalCategories_Currency_CurrencyId",
                table: "functionalCategories",
                column: "CurrencyId",
                principalTable: "Currency",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobDescription_functionalCategories_FunctionalCategoriesId",
                table: "JobDescription",
                column: "FunctionalCategoriesId",
                principalTable: "functionalCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobDescription_jobRanks_JobRanksId",
                table: "JobDescription",
                column: "JobRanksId",
                principalTable: "jobRanks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
