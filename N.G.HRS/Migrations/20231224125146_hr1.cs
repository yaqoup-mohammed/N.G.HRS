using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class hr1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "additionalaccountanformation",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeekendParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OfficialHolidaysParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NightPeriodParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InDayParameter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<TimeOnly>(type: "time", nullable: false),
                    To = table.Column<TimeOnly>(type: "time", nullable: false),
                    Day = table.Column<DateOnly>(type: "date", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additionalaccountanformation", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "allowancesanddiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    AddedToAllEmployees = table.Column<bool>(type: "bit", nullable: false),
                    CumulativeAllowance = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToInsurance = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allowancesanddiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "archives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_archives", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "basicdataforthesalarystatement",
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
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    PercentageOnEmployee = table.Column<int>(type: "int", nullable: false),
                    PercentageOnCompany = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basicdataforthesalarystatement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "basicdataforwagesandsalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfMonthsDays = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AbsencePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DelayPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneFingerPrintPerHourDelay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TypeOfWage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basicdataforwagesandsalaries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "departmentaccounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departmentaccounts", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfEmployment = table.Column<DateOnly>(type: "date", nullable: false),
                    PlacementDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RehireDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfStoppingWork = table.Column<DateOnly>(type: "date", nullable: false),
                    UsedFingerprint = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToInsurance = table.Column<bool>(type: "bit", nullable: false),
                    DateInsurance = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "employeeaccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employeeaccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "financialstatements",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureOfEmployment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceAccountNumber = table.Column<int>(type: "int", nullable: false),
                    BankAccountNumber = table.Column<int>(type: "int", nullable: false),
                    SalaryStartDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SalaryEndDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financialstatements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "guarantees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameOfTheBusiness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommercialRegistrationNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShopAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HomeAdress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberOfDependents = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guarantees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "personaldata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardExpiryTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personaldata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "practicalexperiences",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperiencesType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlacToGainExperience = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practicalexperiences", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "qualifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceivedDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "statementofemployeefiles",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilesStatus = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statementofemployeefiles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "trainingcourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCourses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WhereToGetIt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingcourses", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "additionalaccountanformation");

            migrationBuilder.DropTable(
                name: "allowancesanddiscounts");

            migrationBuilder.DropTable(
                name: "archives");

            migrationBuilder.DropTable(
                name: "basicdataforthesalarystatement");

            migrationBuilder.DropTable(
                name: "basicdataforwagesandsalaries");

            migrationBuilder.DropTable(
                name: "departmentaccounts");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "employeeaccounts");

            migrationBuilder.DropTable(
                name: "financialstatements");

            migrationBuilder.DropTable(
                name: "guarantees");

            migrationBuilder.DropTable(
                name: "personaldata");

            migrationBuilder.DropTable(
                name: "practicalexperiences");

            migrationBuilder.DropTable(
                name: "qualifications");

            migrationBuilder.DropTable(
                name: "statementofemployeefiles");

            migrationBuilder.DropTable(
                name: "trainingcourses");
        }
    }
}
