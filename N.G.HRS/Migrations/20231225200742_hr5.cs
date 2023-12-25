using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class hr5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "additionalaccountanformation");

            migrationBuilder.DropTable(
                name: "basicdataforthesalarystatement");

            migrationBuilder.DropTable(
                name: "personaldata");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainingcourses",
                table: "trainingcourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_statementofemployeefiles",
                table: "statementofemployeefiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_practicalexperiences",
                table: "practicalexperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_financialstatements",
                table: "financialstatements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employeeaccounts",
                table: "employeeaccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departmentaccounts",
                table: "departmentaccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_basicdataforwagesandsalaries",
                table: "basicdataforwagesandsalaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_allowancesanddiscounts",
                table: "allowancesanddiscounts");

            migrationBuilder.DropColumn(
                name: "ExperiencesType",
                table: "practicalexperiences");

            migrationBuilder.RenameTable(
                name: "trainingcourses",
                newName: "trainingCourses");

            migrationBuilder.RenameTable(
                name: "statementofemployeefiles",
                newName: "statementOfEmployeeFiles");

            migrationBuilder.RenameTable(
                name: "practicalexperiences",
                newName: "practicalExperiences");

            migrationBuilder.RenameTable(
                name: "financialstatements",
                newName: "financialStatements");

            migrationBuilder.RenameTable(
                name: "employeeaccounts",
                newName: "employeeAccounts");

            migrationBuilder.RenameTable(
                name: "departmentaccounts",
                newName: "departmentAccounts");

            migrationBuilder.RenameTable(
                name: "basicdataforwagesandsalaries",
                newName: "basicDataForWagesAndSalaries");

            migrationBuilder.RenameTable(
                name: "allowancesanddiscounts",
                newName: "allowancesAndDiscounts");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "statementOfEmployeeFiles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "practicalExperiences",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "financialStatements",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "WhereToGetIt",
                table: "trainingCourses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameCourses",
                table: "trainingCourses",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "statementOfEmployeeFiles",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PlacToGainExperience",
                table: "practicalExperiences",
                type: "nvarchar(70)",
                maxLength: 70,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "practicalExperiences",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "ExperiencesName",
                table: "practicalExperiences",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ShopAddress",
                table: "guarantees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "guarantees",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "guarantees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NameOfTheBusiness",
                table: "guarantees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "guarantees",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HomeAdress",
                table: "guarantees",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "CommercialRegistrationNo",
                table: "guarantees",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SalaryStartDate",
                table: "financialStatements",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "SalaryEndDate",
                table: "financialStatements",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "financialStatements",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "NatureOfEmployment",
                table: "financialStatements",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "employeeAccounts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "employee",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "departmentAccounts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "basicDataForWagesAndSalaries",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "archives",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "allowancesAndDiscounts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainingCourses",
                table: "trainingCourses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_statementOfEmployeeFiles",
                table: "statementOfEmployeeFiles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_practicalExperiences",
                table: "practicalExperiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_financialStatements",
                table: "financialStatements",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employeeAccounts",
                table: "employeeAccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departmentAccounts",
                table: "departmentAccounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_basicDataForWagesAndSalaries",
                table: "basicDataForWagesAndSalaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_allowancesAndDiscounts",
                table: "allowancesAndDiscounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "additionalAccountInformation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateOnly>(type: "date", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additionalAccountInformation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "basicDataForTheSalaryStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthInsuranceIncluded = table.Column<bool>(type: "bit", nullable: false),
                    RetirementInsuranceIncluded = table.Column<bool>(type: "bit", nullable: false),
                    IncludesTheWorkShareInRetirementInsurance = table.Column<bool>(type: "bit", nullable: false),
                    IncludesTaxCalculation = table.Column<bool>(type: "bit", nullable: false),
                    TaxFrom = table.Column<bool>(type: "bit", nullable: false),
                    AllowancesIncluded = table.Column<bool>(type: "bit", nullable: false),
                    IncludesAdditionalData = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    PercentageOnEmployee = table.Column<int>(type: "int", nullable: false),
                    PercentageOnCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basicDataForTheSalaryStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "boardOfDirectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CouncilName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NameOfMembership = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boardOfDirectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "branches",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchesName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BranchesAdress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    BranchesPhone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    BranchesEmail = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LicenseNumber = table.Column<int>(type: "int", nullable: false),
                    TypeOfBusinessActivity = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ComponyLogo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ComponyAdress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contracts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "contractTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StatementOfConditions = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractTerms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "directorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "educationalQualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educationalQualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fingerprintDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DevicesName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConnectionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfPurchase = table.Column<DateOnly>(type: "date", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VendorPhon = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    VendorAdress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ManufactureCompany = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeviceSpecifications = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fingerprintDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "functionalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriesName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functionalCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "functionalClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functionalClasses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "functionalFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functionalFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "governorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_governorates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "jobDescriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JopName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    JobQualifications = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Authorities = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobDescriptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "jobRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jobRanks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "maritalStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_maritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "membershipOfTheBoardOfs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeOFMembership = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membershipOfTheBoardOfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "nationality",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NationalityName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_nationality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "officialVacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VacationsName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_officialVacations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermissionName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PermissionStatus = table.Column<bool>(type: "bit", nullable: false),
                    PermissionsDuration = table.Column<int>(type: "int", nullable: false),
                    RepeatPermissionDuringTheMonth = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "personalDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardExpiryDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalDatas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publicAdministrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicAdministrationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicAdministrations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publicHolidays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HolidayName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    VacationsBalance = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: false),
                    RotationDuration = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicHolidays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "relativesTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelativeName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_relativesTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "religion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_religion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorsName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sectors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "sex",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sex", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "specialties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_specialties", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "additionalAccountInformation");

            migrationBuilder.DropTable(
                name: "basicDataForTheSalaryStatements");

            migrationBuilder.DropTable(
                name: "boardOfDirectors");

            migrationBuilder.DropTable(
                name: "branches");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "contractTerms");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "directorates");

            migrationBuilder.DropTable(
                name: "educationalQualifications");

            migrationBuilder.DropTable(
                name: "fingerprintDevices");

            migrationBuilder.DropTable(
                name: "functionalCategories");

            migrationBuilder.DropTable(
                name: "functionalClasses");

            migrationBuilder.DropTable(
                name: "functionalFiles");

            migrationBuilder.DropTable(
                name: "governorates");

            migrationBuilder.DropTable(
                name: "jobDescriptions");

            migrationBuilder.DropTable(
                name: "jobRanks");

            migrationBuilder.DropTable(
                name: "maritalStatuses");

            migrationBuilder.DropTable(
                name: "membershipOfTheBoardOfs");

            migrationBuilder.DropTable(
                name: "nationality");

            migrationBuilder.DropTable(
                name: "officialVacations");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "personalDatas");

            migrationBuilder.DropTable(
                name: "publicAdministrations");

            migrationBuilder.DropTable(
                name: "publicHolidays");

            migrationBuilder.DropTable(
                name: "relativesTypes");

            migrationBuilder.DropTable(
                name: "religion");

            migrationBuilder.DropTable(
                name: "sectors");

            migrationBuilder.DropTable(
                name: "sex");

            migrationBuilder.DropTable(
                name: "specialties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_trainingCourses",
                table: "trainingCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_statementOfEmployeeFiles",
                table: "statementOfEmployeeFiles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_practicalExperiences",
                table: "practicalExperiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_financialStatements",
                table: "financialStatements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_employeeAccounts",
                table: "employeeAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_departmentAccounts",
                table: "departmentAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_basicDataForWagesAndSalaries",
                table: "basicDataForWagesAndSalaries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_allowancesAndDiscounts",
                table: "allowancesAndDiscounts");

            migrationBuilder.DropColumn(
                name: "ExperiencesName",
                table: "practicalExperiences");

            migrationBuilder.RenameTable(
                name: "trainingCourses",
                newName: "trainingcourses");

            migrationBuilder.RenameTable(
                name: "statementOfEmployeeFiles",
                newName: "statementofemployeefiles");

            migrationBuilder.RenameTable(
                name: "practicalExperiences",
                newName: "practicalexperiences");

            migrationBuilder.RenameTable(
                name: "financialStatements",
                newName: "financialstatements");

            migrationBuilder.RenameTable(
                name: "employeeAccounts",
                newName: "employeeaccounts");

            migrationBuilder.RenameTable(
                name: "departmentAccounts",
                newName: "departmentaccounts");

            migrationBuilder.RenameTable(
                name: "basicDataForWagesAndSalaries",
                newName: "basicdataforwagesandsalaries");

            migrationBuilder.RenameTable(
                name: "allowancesAndDiscounts",
                newName: "allowancesanddiscounts");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "statementofemployeefiles",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "practicalexperiences",
                newName: "ID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "financialstatements",
                newName: "ID");

            migrationBuilder.AlterColumn<string>(
                name: "WhereToGetIt",
                table: "trainingcourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "NameCourses",
                table: "trainingcourses",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(150)",
                oldMaxLength: 150);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "statementofemployeefiles",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PlacToGainExperience",
                table: "practicalexperiences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(70)",
                oldMaxLength: 70);

            migrationBuilder.AlterColumn<string>(
                name: "Duration",
                table: "practicalexperiences",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<string>(
                name: "ExperiencesType",
                table: "practicalexperiences",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "ShopAddress",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "NameOfTheBusiness",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "HomeAdress",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "CommercialRegistrationNo",
                table: "guarantees",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SalaryStartDate",
                table: "financialstatements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "SalaryEndDate",
                table: "financialstatements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "financialstatements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NatureOfEmployment",
                table: "financialstatements",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "employeeaccounts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255);

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "employee",
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
                table: "departmentaccounts",
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
                table: "basicdataforwagesandsalaries",
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
                table: "archives",
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
                table: "allowancesanddiscounts",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(255)",
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_trainingcourses",
                table: "trainingcourses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_statementofemployeefiles",
                table: "statementofemployeefiles",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_practicalexperiences",
                table: "practicalexperiences",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_financialstatements",
                table: "financialstatements",
                column: "ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_employeeaccounts",
                table: "employeeaccounts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_departmentaccounts",
                table: "departmentaccounts",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_basicdataforwagesandsalaries",
                table: "basicdataforwagesandsalaries",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_allowancesanddiscounts",
                table: "allowancesanddiscounts",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "additionalaccountanformation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<DateOnly>(type: "date", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FromTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    InDayParameter = table.Column<int>(type: "int", nullable: false),
                    NightPeriodParameter = table.Column<int>(type: "int", nullable: false),
                    NormalParameter = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    OfficialHolidaysParameter = table.Column<int>(type: "int", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    WeekendParameter = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additionalaccountanformation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "basicdataforthesalarystatement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AllowancesIncluded = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    HealthInsuranceIncluded = table.Column<bool>(type: "bit", nullable: false),
                    IncludesAdditionalData = table.Column<bool>(type: "bit", nullable: false),
                    IncludesTaxCalculation = table.Column<bool>(type: "bit", nullable: false),
                    IncludesTheWorkShareInRetirementInsurance = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    PercentageOnCompany = table.Column<int>(type: "int", nullable: false),
                    PercentageOnEmployee = table.Column<int>(type: "int", nullable: false),
                    RetirementInsuranceIncluded = table.Column<bool>(type: "bit", nullable: false),
                    TaxFrom = table.Column<bool>(type: "bit", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basicdataforthesalarystatement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "personaldata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    CardExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personaldata", x => x.Id);
                });
        }
    }
}
