using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class B1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AllowancesIncluded",
                table: "additionalAccountInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HealthInsuranceIncluded",
                table: "additionalAccountInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludesAdditionalData",
                table: "additionalAccountInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludesRetirementInsurance",
                table: "additionalAccountInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludesTaxCalculation",
                table: "additionalAccountInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IncludesTheWorkShareInRetirementInsurance",
                table: "additionalAccountInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PercentageOnTheCompany",
                table: "additionalAccountInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PercentageOnTheEmployee",
                table: "additionalAccountInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TaxFrom",
                table: "additionalAccountInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "percentage",
                table: "additionalAccountInformation",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AllowancesIncluded",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "HealthInsuranceIncluded",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "IncludesAdditionalData",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "IncludesRetirementInsurance",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "IncludesTaxCalculation",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "IncludesTheWorkShareInRetirementInsurance",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "PercentageOnTheCompany",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "PercentageOnTheEmployee",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "TaxFrom",
                table: "additionalAccountInformation");

            migrationBuilder.DropColumn(
                name: "percentage",
                table: "additionalAccountInformation");
        }
    }
}
