using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "additionalAccountInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NormalCoefficient = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    WeekendLaboratories = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OfficialHolidaysLab = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    NightPeriodParameter = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LaboratoriesPerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_additionalAccountInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceLog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnrollNumber = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Section = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MachineNo = table.Column<int>(type: "int", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false),
                    SaveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProcessedBeFore = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "basicDataForTheSalaryStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthInsuranceIncluded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RetirementInsuranceIncluded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncludesTheWorkShareInRetirementInsurance = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncludesTaxCalculation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaxFrom = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AllowancesIncluded = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IncludesAdditionalData = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: true),
                    PercentageOnEmployee = table.Column<int>(type: "int", nullable: true),
                    PercentageOnCompany = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basicDataForTheSalaryStatements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "basicDataForWagesAndSalaries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfMonthsDays = table.Column<int>(type: "int", nullable: true),
                    AbsencePerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DelayPerHour = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OneFingerPrintPerHourDelay = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    TypeOfWage = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_basicDataForWagesAndSalaries", x => x.Id);
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
                name: "country",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Data = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_country", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CurrencyName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    CurrencyNotes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CurrentPriceOfCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PreviousPriceOfCurrency = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
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
                name: "FinanceAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceAccount", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FinanceAccountType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountNumber = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceAccountType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "fingerprintDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DevicesName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    DevicesNumber = table.Column<int>(type: "int", nullable: false),
                    DeviceType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DeviceStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ConnectionType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DateOfPurchase = table.Column<DateOnly>(type: "date", nullable: false),
                    VendorName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    VendorPhon = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    VendorAdress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ManufactureCompany = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    DeviceSpecifications = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fingerprintDevices", x => x.Id);
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
                name: "jobRanks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RankName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
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
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_membershipOfTheBoardOfs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "months",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Month = table.Column<DateOnly>(type: "date", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_months", x => x.Id);
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
                name: "permanenceModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermanenceName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FlexibleWorkingHours = table.Column<bool>(type: "bit", nullable: false),
                    WorkBetweenTwoShifts = table.Column<bool>(type: "bit", nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    HoursOfWorks = table.Column<double>(type: "float", nullable: true),
                    AddAttendanceAndDeparturePermission = table.Column<bool>(type: "bit", nullable: false),
                    AllowanceForLateAttendance = table.Column<int>(type: "int", nullable: true),
                    EarlyDeparturePermission = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permanenceModels", x => x.Id);
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
                name: "permissionToAttendAndLeaves",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PermanencyStatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_permissionToAttendAndLeaves", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "publicAdministrations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicAdministrationName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Nots = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
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
                    Balance = table.Column<bool>(type: "bit", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: true),
                    RotationDuration = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publicHolidays", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Universities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Violations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolationsName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Violations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "contractTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModelName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    StatementOfConditions = table.Column<string>(type: "text", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ContractsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_contractTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_contractTerms_contracts_ContractsId",
                        column: x => x.ContractsId,
                        principalTable: "contracts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "governorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_governorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_governorates_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "allowancesAndDiscounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(170)", maxLength: 170, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    AddedToAllEmployees = table.Column<bool>(type: "bit", nullable: false),
                    CumulativeAllowance = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToInsurance = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_allowancesAndDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_allowancesAndDiscounts_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "functionalCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoriesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functionalCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_functionalCategories_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "functionalClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_functionalClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_functionalClasses_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "guarantees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    NameOfTheBusiness = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CommercialRegistrationNo = table.Column<int>(type: "int", nullable: false),
                    ShopAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    HomeAdress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    NumberOfDependents = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_guarantees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_guarantees_maritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "maritalStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "boardOfDirectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    CouncilName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NameOfMembership = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    MembershipOfTheBoardOfDirectorsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boardOfDirectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_boardOfDirectors_membershipOfTheBoardOfs_MembershipOfTheBoardOfDirectorsId",
                        column: x => x.MembershipOfTheBoardOfDirectorsId,
                        principalTable: "membershipOfTheBoardOfs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PeriodsName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Saturday = table.Column<bool>(type: "bit", nullable: false),
                    SunDay = table.Column<bool>(type: "bit", nullable: false),
                    Monday = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    Muinutes = table.Column<int>(type: "int", nullable: true),
                    PermanenceModelsId = table.Column<int>(type: "int", nullable: true),
                    PeriodsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Periods_Periods_PeriodsId",
                        column: x => x.PeriodsId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Periods_permanenceModels_PermanenceModelsId",
                        column: x => x.PermanenceModelsId,
                        principalTable: "permanenceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EducationalQualificationAndQualification",
                columns: table => new
                {
                    EducationalQualificationId = table.Column<int>(type: "int", nullable: false),
                    qualificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EducationalQualificationAndQualification", x => new { x.EducationalQualificationId, x.qualificationsId });
                    table.ForeignKey(
                        name: "FK_EducationalQualificationAndQualification_educationalQualifications_EducationalQualificationId",
                        column: x => x.EducationalQualificationId,
                        principalTable: "educationalQualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EducationalQualificationAndQualification_qualifications_qualificationsId",
                        column: x => x.qualificationsId,
                        principalTable: "qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpecialtiesAndQualification",
                columns: table => new
                {
                    SpecialtiesId = table.Column<int>(type: "int", nullable: false),
                    qualificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialtiesAndQualification", x => new { x.SpecialtiesId, x.qualificationsId });
                    table.ForeignKey(
                        name: "FK_SpecialtiesAndQualification_qualifications_qualificationsId",
                        column: x => x.qualificationsId,
                        principalTable: "qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecialtiesAndQualification_specialties_SpecialtiesId",
                        column: x => x.SpecialtiesId,
                        principalTable: "specialties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniversitiesAndQualification",
                columns: table => new
                {
                    qualificationsId = table.Column<int>(type: "int", nullable: false),
                    universitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversitiesAndQualification", x => new { x.qualificationsId, x.universitiesId });
                    table.ForeignKey(
                        name: "FK_UniversitiesAndQualification_Universities_universitiesId",
                        column: x => x.universitiesId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversitiesAndQualification_qualifications_qualificationsId",
                        column: x => x.qualificationsId,
                        principalTable: "qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "directorates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    GovernorateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_directorates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_directorates_governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "governorates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobDescription",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JopName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    JobQualifications = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Authorities = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Responsibilities = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FunctionalCategoriesId = table.Column<int>(type: "int", nullable: true),
                    FunctionalClassId = table.Column<int>(type: "int", nullable: true),
                    JobRanksId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobDescription_functionalCategories_FunctionalCategoriesId",
                        column: x => x.FunctionalCategoriesId,
                        principalTable: "functionalCategories",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobDescription_functionalClasses_FunctionalClassId",
                        column: x => x.FunctionalClassId,
                        principalTable: "functionalClasses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_JobDescription_jobRanks_JobRanksId",
                        column: x => x.JobRanksId,
                        principalTable: "jobRanks",
                        principalColumn: "Id");
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
                    ComponyAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BoardOfDirectorsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_company", x => x.Id);
                    table.ForeignKey(
                        name: "FK_company_boardOfDirectors_BoardOfDirectorsId",
                        column: x => x.BoardOfDirectorsId,
                        principalTable: "boardOfDirectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "adjustingTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StaffTimeStatues = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PermanenceModelsId = table.Column<int>(type: "int", nullable: false),
                    PeriodsId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adjustingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adjustingTimes_Periods_PeriodsId",
                        column: x => x.PeriodsId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_adjustingTimes_permanenceModels_PermanenceModelsId",
                        column: x => x.PermanenceModelsId,
                        principalTable: "permanenceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "weekends",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SaturDay = table.Column<bool>(type: "bit", nullable: false),
                    SunDay = table.Column<bool>(type: "bit", nullable: false),
                    MonDay = table.Column<bool>(type: "bit", nullable: false),
                    Tuesday = table.Column<bool>(type: "bit", nullable: false),
                    Wednesday = table.Column<bool>(type: "bit", nullable: false),
                    Thursday = table.Column<bool>(type: "bit", nullable: false),
                    Friday = table.Column<bool>(type: "bit", nullable: false),
                    PermanenceModelsId = table.Column<int>(type: "int", nullable: true),
                    PeriodsId = table.Column<int>(type: "int", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(34)", maxLength: 34, nullable: false),
                    NumbersOfHours = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_weekends", x => x.Id);
                    table.ForeignKey(
                        name: "FK_weekends_Periods_PeriodsId",
                        column: x => x.PeriodsId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_weekends_permanenceModels_PermanenceModelsId",
                        column: x => x.PermanenceModelsId,
                        principalTable: "permanenceModels",
                        principalColumn: "Id");
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
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CompanyId = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    GovernorateId = table.Column<int>(type: "int", nullable: true),
                    DirectorateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_branches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_branches_company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "company",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_branches_country_CountryId",
                        column: x => x.CountryId,
                        principalTable: "country",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_branches_directorates_DirectorateId",
                        column: x => x.DirectorateId,
                        principalTable: "directorates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_branches_governorates_GovernorateId",
                        column: x => x.GovernorateId,
                        principalTable: "governorates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "sectors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectorsName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    BranchesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sectors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_sectors_branches_BranchesId",
                        column: x => x.BranchesId,
                        principalTable: "branches",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubAdministration = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SectorsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_sectors_SectorsId",
                        column: x => x.SectorsId,
                        principalTable: "sectors",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionsName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DepartmentsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sections_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(170)", maxLength: 170, nullable: false),
                    DateOfEmployment = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PlacementDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmploymentStatus = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RehireDate = table.Column<DateOnly>(type: "date", nullable: true),
                    DateOfStoppingWork = table.Column<DateOnly>(type: "date", nullable: true),
                    UsedFingerprint = table.Column<bool>(type: "bit", nullable: false),
                    SubjectToInsurance = table.Column<bool>(type: "bit", nullable: false),
                    DateInsurance = table.Column<DateOnly>(type: "date", nullable: true),
                    FingerPrintImage = table.Column<byte>(type: "tinyint", nullable: false),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    SectionsId = table.Column<int>(type: "int", nullable: false),
                    JobDescriptionId = table.Column<int>(type: "int", nullable: false),
                    FingerprintDevicesId = table.Column<int>(type: "int", nullable: true),
                    ManagerId = table.Column<int>(type: "int", nullable: true),
                    CurrentJop = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employee", x => x.Id);
                    table.ForeignKey(
                        name: "FK_employee_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_JobDescription_JobDescriptionId",
                        column: x => x.JobDescriptionId,
                        principalTable: "JobDescription",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_employee_employee_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_employee_fingerprintDevices_FingerprintDevicesId",
                        column: x => x.FingerprintDevicesId,
                        principalTable: "fingerprintDevices",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MachineNumber = table.Column<int>(type: "int", nullable: false),
                    EmployeeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IndRegID = table.Column<int>(type: "int", nullable: false),
                    DateTimeRecord = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOnlyRecord = table.Column<DateOnly>(type: "date", nullable: false),
                    TimeOnlyRecord = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsProcessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineInfo_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineInfo_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SectionsAccounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    FinanceAccountTypeId = table.Column<int>(type: "int", nullable: true),
                    FinanceAccountId = table.Column<int>(type: "int", nullable: true),
                    SectionsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectionsAccounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_SectionsAccounts_FinanceAccountType_FinanceAccountTypeId",
                        column: x => x.FinanceAccountTypeId,
                        principalTable: "FinanceAccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SectionsAccounts_FinanceAccount_FinanceAccountId",
                        column: x => x.FinanceAccountId,
                        principalTable: "FinanceAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SectionsAccounts_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalExternalOfWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    SubstituteEmployeeId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    TotalHours = table.Column<int>(type: "int", nullable: false),
                    Minutes = table.Column<int>(type: "int", nullable: false),
                    Mission = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskDestination = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssignmentId = table.Column<int>(type: "int", nullable: false),
                    BetweenToDate = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProccessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalExternalOfWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalExternalOfWork_Assignment_AssignmentId",
                        column: x => x.AssignmentId,
                        principalTable: "Assignment",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdditionalExternalOfWork_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdditionalExternalOfWork_employee_SubstituteEmployeeId",
                        column: x => x.SubstituteEmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdditionalUnsupportedEmployees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AdditionalUnsupported = table.Column<int>(type: "int", nullable: false),
                    AdditionalSupported = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    migration = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdditionalUnsupportedEmployees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdditionalUnsupportedEmployees_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AdministrativeDecisions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    SalaryStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SalaryEndtDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Salary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    EmployeementReson = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DecisionsType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeementOn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativeDecisions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativeDecisions_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdministrativeDecisions_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdministrativePromotions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdministrativePromotions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdministrativePromotions_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdministrativePromotions_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AnnualGoals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Goals = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualGoals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualGoals_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceAndAbsenceProcessing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    AttendanceStatusId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    ToTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    TotalWorkMinutes = table.Column<int>(type: "int", nullable: true),
                    MinutesOfLate = table.Column<int>(type: "int", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    periodId = table.Column<int>(type: "int", nullable: true),
                    permenenceId = table.Column<int>(type: "int", nullable: true),
                    IsProcssessed = table.Column<bool>(type: "bit", nullable: false),
                    IsProcssessedBefore = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceAndAbsenceProcessing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceAndAbsenceProcessing_AttendanceStatus_AttendanceStatusId",
                        column: x => x.AttendanceStatusId,
                        principalTable: "AttendanceStatus",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceAndAbsenceProcessing_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceAndAbsenceProcessing_Periods_periodId",
                        column: x => x.periodId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceAndAbsenceProcessing_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceAndAbsenceProcessing_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceAndAbsenceProcessing_permanenceModels_permenenceId",
                        column: x => x.permenenceId,
                        principalTable: "permanenceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AttendanceRecord",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PeriodsId = table.Column<int>(type: "int", nullable: true),
                    TimeOnlyRecord = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_Periods_PeriodsId",
                        column: x => x.PeriodsId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AttendanceRecord_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AutomaticallyApprovedAdd_on",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SectionsId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    FromTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    ToTime = table.Column<TimeOnly>(type: "time", nullable: false),
                    Hours = table.Column<double>(type: "float", nullable: true),
                    Minutes = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AutomaticallyApprovedAdd_on", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AutomaticallyApprovedAdd_on_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    FinanceAccountTypeId = table.Column<int>(type: "int", nullable: true),
                    FinanceAccountId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAccount_FinanceAccountType_FinanceAccountTypeId",
                        column: x => x.FinanceAccountTypeId,
                        principalTable: "FinanceAccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeAccount_FinanceAccount_FinanceAccountId",
                        column: x => x.FinanceAccountId,
                        principalTable: "FinanceAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeAccount_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeArchives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descriotion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    File = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeArchives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeArchives_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLoans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstallmentStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Arrest = table.Column<bool>(type: "bit", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    InstallmentAmount = table.Column<double>(type: "float", nullable: false),
                    NumberOfInstallmentMonths = table.Column<double>(type: "float", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLoans", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLoans_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeLoans_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeMovements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateDown = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    jopdescriptionId = table.Column<int>(type: "int", nullable: true),
                    CurrentJop = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastJop = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeMovements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeMovements_JobDescription_jopdescriptionId",
                        column: x => x.jopdescriptionId,
                        principalTable: "JobDescription",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeMovements_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeePerks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Percentage = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePerks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePerks_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeePermissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    SupervisorId = table.Column<int>(type: "int", nullable: true),
                    PeriodId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FromTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: true),
                    Hours = table.Column<double>(type: "float", nullable: true),
                    Minutes = table.Column<int>(type: "int", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BetweenToDate = table.Column<bool>(type: "bit", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsProccessed = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeePermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeePermissions_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePermissions_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePermissions_employee_SupervisorId",
                        column: x => x.SupervisorId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeePermissions_permissions_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "permissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeesQualifications",
                columns: table => new
                {
                    employeesId = table.Column<int>(type: "int", nullable: false),
                    qualificationsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeesQualifications", x => new { x.employeesId, x.qualificationsId });
                    table.ForeignKey(
                        name: "FK_EmployeesQualifications_employee_employeesId",
                        column: x => x.employeesId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeesQualifications_qualifications_qualificationsId",
                        column: x => x.qualificationsId,
                        principalTable: "qualifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentStatusManagement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StatusDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentStatusManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmploymentStatusManagement_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EndOfServiceClearance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndOfServiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    ReasonForClearance = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastApprovedSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ServicePeriodPerYear = table.Column<int>(type: "int", nullable: false),
                    EndOfServiceBenefits = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AdvancesAndLoans = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    VacationEntitlements = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Absence = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherEntitlements = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OtherDiscounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EndOfServiceClearance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EndOfServiceClearance_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntitlementsAndDeductions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Month = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Taxable = table.Column<bool>(type: "bit", nullable: false),
                    FinanceAccountTypeId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<double>(type: "float", nullable: true),
                    Percentage = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntitlementsAndDeductions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EntitlementsAndDeductions_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntitlementsAndDeductions_FinanceAccountType_FinanceAccountTypeId",
                        column: x => x.FinanceAccountTypeId,
                        principalTable: "FinanceAccountType",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EntitlementsAndDeductions_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Family",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    RelativesTypeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Family", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Family_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Family_relativesTypes_RelativesTypeId",
                        column: x => x.RelativesTypeId,
                        principalTable: "relativesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "financialStatements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NatureOfEmployment = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    BasicSalary = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    InsuranceAccountNumber = table.Column<int>(type: "int", nullable: true),
                    BankAccountNumber = table.Column<int>(type: "int", nullable: true),
                    SalaryStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SalaryEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_financialStatements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_financialStatements_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_financialStatements_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "linkingEmployeesToShiftPeriods",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfStartWork = table.Column<DateOnly>(type: "date", nullable: false),
                    DateOfEndWork = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartmentsId = table.Column<int>(type: "int", nullable: true),
                    SectionsId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PermanenceModelsId = table.Column<int>(type: "int", nullable: true),
                    PeriodsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_linkingEmployeesToShiftPeriods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_linkingEmployeesToShiftPeriods_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_linkingEmployeesToShiftPeriods_Periods_PeriodsId",
                        column: x => x.PeriodsId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_linkingEmployeesToShiftPeriods_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_linkingEmployeesToShiftPeriods_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_linkingEmployeesToShiftPeriods_permanenceModels_PermanenceModelsId",
                        column: x => x.PermanenceModelsId,
                        principalTable: "permanenceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "oneFingerprints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    OneDayFingerprint = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_oneFingerprints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_oneFingerprints_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "openingBalancesForVacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BalanceYear = table.Column<int>(type: "int", nullable: false),
                    Balance = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PublicHolidaysId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_openingBalancesForVacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_openingBalancesForVacations_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_openingBalancesForVacations_publicHolidays_PublicHolidaysId",
                        column: x => x.PublicHolidaysId,
                        principalTable: "publicHolidays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Permits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PermitsType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsNotEmployee = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    NotEmployee = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    For = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Purpose = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permits_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "personalDatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    HomePhone = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    CardType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ToRelease = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CardNumber = table.Column<int>(type: "int", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CardExpiryDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    GuaranteesId = table.Column<int>(type: "int", nullable: false),
                    SexId = table.Column<int>(type: "int", nullable: false),
                    NationalityId = table.Column<int>(type: "int", nullable: false),
                    ReligionId = table.Column<int>(type: "int", nullable: false),
                    MaritalStatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personalDatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personalDatas_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personalDatas_guarantees_GuaranteesId",
                        column: x => x.GuaranteesId,
                        principalTable: "guarantees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personalDatas_maritalStatuses_MaritalStatusId",
                        column: x => x.MaritalStatusId,
                        principalTable: "maritalStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_personalDatas_nationality_NationalityId",
                        column: x => x.NationalityId,
                        principalTable: "nationality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personalDatas_religion_ReligionId",
                        column: x => x.ReligionId,
                        principalTable: "religion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personalDatas_sex_SexId",
                        column: x => x.SexId,
                        principalTable: "sex",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "practicalExperiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperiencesName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PlacToGainExperience = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Duration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_practicalExperiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_practicalExperiences_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "staffTimes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorksFullTimeFromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PermanenceModelsId = table.Column<int>(type: "int", nullable: true),
                    SectionsId = table.Column<int>(type: "int", nullable: true),
                    PeriodId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_staffTimes_Periods_PeriodId",
                        column: x => x.PeriodId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_staffTimes_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_staffTimes_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_staffTimes_permanenceModels_PermanenceModelsId",
                        column: x => x.PermanenceModelsId,
                        principalTable: "permanenceModels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffVacations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VacationId = table.Column<int>(type: "int", nullable: true),
                    SectionId = table.Column<int>(type: "int", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    PeriodsId = table.Column<int>(type: "int", nullable: true),
                    IsConnected = table.Column<bool>(type: "bit", nullable: false),
                    FromDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ToDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PerDay = table.Column<int>(type: "int", nullable: false),
                    PerHour = table.Column<int>(type: "int", nullable: false),
                    PerMinute = table.Column<int>(type: "int", nullable: false),
                    Accepted = table.Column<bool>(type: "bit", nullable: false),
                    SubstituteStaffMemberId = table.Column<int>(type: "int", nullable: true),
                    DonorSide = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffVacations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffVacations_Periods_PeriodsId",
                        column: x => x.PeriodsId,
                        principalTable: "Periods",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffVacations_Sections_SectionId",
                        column: x => x.SectionId,
                        principalTable: "Sections",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffVacations_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffVacations_employee_SubstituteStaffMemberId",
                        column: x => x.SubstituteStaffMemberId,
                        principalTable: "employee",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_StaffVacations_publicHolidays_VacationId",
                        column: x => x.VacationId,
                        principalTable: "publicHolidays",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "statementOfEmployeeFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilesStatus = table.Column<bool>(type: "bit", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statementOfEmployeeFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_statementOfEmployeeFiles_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trainingCourses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameCourses = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    WhereToGetIt = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FromDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ToDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trainingCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_trainingCourses_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacationAllowances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EmplyeeId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    VacationBalance = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CarryoverBalance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationAllowances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationAllowances_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VacationBalance",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    Editorial = table.Column<int>(type: "int", nullable: false),
                    Annual = table.Column<int>(type: "int", nullable: false),
                    Transferred = table.Column<int>(type: "int", nullable: false),
                    Expendables = table.Column<int>(type: "int", nullable: false),
                    Residual = table.Column<int>(type: "int", nullable: false),
                    ShiftHour = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacationBalance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacationBalance_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAdvances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false),
                    DepartmentsId = table.Column<int>(type: "int", nullable: false),
                    SectionId = table.Column<int>(type: "int", nullable: false),
                    SectionsId = table.Column<int>(type: "int", nullable: false),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
                    CurrencyId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAdvances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvances_Currency_CurrencyId",
                        column: x => x.CurrencyId,
                        principalTable: "Currency",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvances_Departments_DepartmentsId",
                        column: x => x.DepartmentsId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvances_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeAdvances_Sections_SectionsId",
                        column: x => x.SectionsId,
                        principalTable: "Sections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAdvances_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "FunctionalFilesOfStatementOfEmployeeFiles",
                columns: table => new
                {
                    FunctionalFilesId = table.Column<int>(type: "int", nullable: false),
                    StatementOfEmployeeFilesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionalFilesOfStatementOfEmployeeFiles", x => new { x.FunctionalFilesId, x.StatementOfEmployeeFilesId });
                    table.ForeignKey(
                        name: "FK_FunctionalFilesOfStatementOfEmployeeFiles_functionalFiles_FunctionalFilesId",
                        column: x => x.FunctionalFilesId,
                        principalTable: "functionalFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FunctionalFilesOfStatementOfEmployeeFiles_statementOfEmployeeFiles_StatementOfEmployeeFilesId",
                        column: x => x.StatementOfEmployeeFilesId,
                        principalTable: "statementOfEmployeeFiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeViolations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ViolationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateOnly = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Discounts = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReceiptOfNotifications = table.Column<bool>(type: "bit", nullable: false),
                    Exempt = table.Column<bool>(type: "bit", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    ViolationId = table.Column<int>(type: "int", nullable: true),
                    PenaltiesId = table.Column<int>(type: "int", nullable: true),
                    NumberPenalties = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeViolations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeViolations_Violations_ViolationId",
                        column: x => x.ViolationId,
                        principalTable: "Violations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EmployeeViolations_employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "employee",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Penalties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PenaltiesName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deduction = table.Column<bool>(type: "bit", nullable: false),
                    DiscountFromWorkingHours = table.Column<bool>(type: "bit", nullable: false),
                    DeductionFromTheDailyWage = table.Column<bool>(type: "bit", nullable: false),
                    DeductionFromSalary = table.Column<bool>(type: "bit", nullable: false),
                    Value = table.Column<int>(type: "int", nullable: true),
                    Percent = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PenaltiesAndViolationsFormsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penalties", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "penaltiesAndViolationsForms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    NumberOfTime = table.Column<int>(type: "int", nullable: false),
                    ViolationsId = table.Column<int>(type: "int", nullable: true),
                    PenaltiesId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_penaltiesAndViolationsForms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_penaltiesAndViolationsForms_Penalties_PenaltiesId",
                        column: x => x.PenaltiesId,
                        principalTable: "Penalties",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_penaltiesAndViolationsForms_Violations_ViolationsId",
                        column: x => x.ViolationsId,
                        principalTable: "Violations",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Assignment",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "تكليف إضافي" },
                    { 2, "تكليف خارجي" }
                });

            migrationBuilder.InsertData(
                table: "AttendanceStatus",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "حضور" },
                    { 2, "غياب" },
                    { 3, "سماحية انصراف مبكر" },
                    { 4, "سماحية حضور متأخر" },
                    { 5, "اذن" },
                    { 6, "اجازة" },
                    { 7, "اجازة رسمية" },
                    { 8, "اجازة اسبوعية" },
                    { 9, "إضافي معتمد" },
                    { 10, "إضافي غير معتمد" },
                    { 11, "انصراف بدون عذر" },
                    { 12, "تأخير" },
                    { 13, "غياب نصف يوم" },
                    { 14, "سماحية حضور وانصراف" },
                    { 15, "تكليف خارجي " }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalExternalOfWork_AssignmentId",
                table: "AdditionalExternalOfWork",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalExternalOfWork_EmployeeId",
                table: "AdditionalExternalOfWork",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalExternalOfWork_SubstituteEmployeeId",
                table: "AdditionalExternalOfWork",
                column: "SubstituteEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdditionalUnsupportedEmployees_EmployeeId",
                table: "AdditionalUnsupportedEmployees",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_adjustingTimes_PeriodsId",
                table: "adjustingTimes",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_adjustingTimes_PermanenceModelsId",
                table: "adjustingTimes",
                column: "PermanenceModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeDecisions_CurrencyId",
                table: "AdministrativeDecisions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativeDecisions_EmployeeId",
                table: "AdministrativeDecisions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativePromotions_DepartmentsId",
                table: "AdministrativePromotions",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_AdministrativePromotions_EmployeeId",
                table: "AdministrativePromotions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_allowancesAndDiscounts_CurrencyId",
                table: "allowancesAndDiscounts",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualGoals_EmployeeId",
                table: "AnnualGoals",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAndAbsenceProcessing_AttendanceStatusId",
                table: "AttendanceAndAbsenceProcessing",
                column: "AttendanceStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAndAbsenceProcessing_DepartmentId",
                table: "AttendanceAndAbsenceProcessing",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAndAbsenceProcessing_EmployeeId",
                table: "AttendanceAndAbsenceProcessing",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAndAbsenceProcessing_periodId",
                table: "AttendanceAndAbsenceProcessing",
                column: "periodId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAndAbsenceProcessing_permenenceId",
                table: "AttendanceAndAbsenceProcessing",
                column: "permenenceId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceAndAbsenceProcessing_SectionId",
                table: "AttendanceAndAbsenceProcessing",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_EmployeeId",
                table: "AttendanceRecord",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_PeriodsId",
                table: "AttendanceRecord",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceRecord_SectionId",
                table: "AttendanceRecord",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_AutomaticallyApprovedAdd_on_EmployeeId",
                table: "AutomaticallyApprovedAdd_on",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AutomaticallyApprovedAdd_on_SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_boardOfDirectors_MembershipOfTheBoardOfDirectorsId",
                table: "boardOfDirectors",
                column: "MembershipOfTheBoardOfDirectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_branches_CompanyId",
                table: "branches",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_branches_CountryId",
                table: "branches",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_branches_DirectorateId",
                table: "branches",
                column: "DirectorateId");

            migrationBuilder.CreateIndex(
                name: "IX_branches_GovernorateId",
                table: "branches",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_company_BoardOfDirectorsId",
                table: "company",
                column: "BoardOfDirectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_contractTerms_ContractsId",
                table: "contractTerms",
                column: "ContractsId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_SectorsId",
                table: "Departments",
                column: "SectorsId");

            migrationBuilder.CreateIndex(
                name: "IX_directorates_GovernorateId",
                table: "directorates",
                column: "GovernorateId");

            migrationBuilder.CreateIndex(
                name: "IX_EducationalQualificationAndQualification_qualificationsId",
                table: "EducationalQualificationAndQualification",
                column: "qualificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_DepartmentsId",
                table: "employee",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_FingerprintDevicesId",
                table: "employee",
                column: "FingerprintDevicesId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_JobDescriptionId",
                table: "employee",
                column: "JobDescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_ManagerId",
                table: "employee",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_SectionsId",
                table: "employee",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAccount_EmployeeId",
                table: "EmployeeAccount",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAccount_FinanceAccountId",
                table: "EmployeeAccount",
                column: "FinanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAccount_FinanceAccountTypeId",
                table: "EmployeeAccount",
                column: "FinanceAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvances_CurrencyId",
                table: "EmployeeAdvances",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvances_DepartmentsId",
                table: "EmployeeAdvances",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvances_EmployeeAccountId",
                table: "EmployeeAdvances",
                column: "EmployeeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvances_EmployeeId",
                table: "EmployeeAdvances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAdvances_SectionsId",
                table: "EmployeeAdvances",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeArchives_EmployeeId",
                table: "EmployeeArchives",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLoans_CurrencyId",
                table: "EmployeeLoans",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLoans_EmployeeId",
                table: "EmployeeLoans",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMovements_EmployeeId",
                table: "EmployeeMovements",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeMovements_jopdescriptionId",
                table: "EmployeeMovements",
                column: "jopdescriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePerks_EmployeeId",
                table: "EmployeePerks",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermissions_EmployeeId",
                table: "EmployeePermissions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermissions_PeriodId",
                table: "EmployeePermissions",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermissions_PermissionId",
                table: "EmployeePermissions",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePermissions_SupervisorId",
                table: "EmployeePermissions",
                column: "SupervisorId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeesQualifications_qualificationsId",
                table: "EmployeesQualifications",
                column: "qualificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeViolations_EmployeeId",
                table: "EmployeeViolations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeViolations_PenaltiesId",
                table: "EmployeeViolations",
                column: "PenaltiesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeViolations_ViolationId",
                table: "EmployeeViolations",
                column: "ViolationId");

            migrationBuilder.CreateIndex(
                name: "IX_EmploymentStatusManagement_EmployeeId",
                table: "EmploymentStatusManagement",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EndOfServiceClearance_EmployeeId",
                table: "EndOfServiceClearance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitlementsAndDeductions_CurrencyId",
                table: "EntitlementsAndDeductions",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitlementsAndDeductions_EmployeeId",
                table: "EntitlementsAndDeductions",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EntitlementsAndDeductions_FinanceAccountTypeId",
                table: "EntitlementsAndDeductions",
                column: "FinanceAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Family_EmployeeId",
                table: "Family",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Family_RelativesTypeId",
                table: "Family",
                column: "RelativesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_financialStatements_CurrencyId",
                table: "financialStatements",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_financialStatements_EmployeeId",
                table: "financialStatements",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_functionalCategories_CurrencyId",
                table: "functionalCategories",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_functionalClasses_CurrencyId",
                table: "functionalClasses",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_FunctionalFilesOfStatementOfEmployeeFiles_StatementOfEmployeeFilesId",
                table: "FunctionalFilesOfStatementOfEmployeeFiles",
                column: "StatementOfEmployeeFilesId");

            migrationBuilder.CreateIndex(
                name: "IX_governorates_CountryId",
                table: "governorates",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_guarantees_MaritalStatusId",
                table: "guarantees",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDescription_FunctionalCategoriesId",
                table: "JobDescription",
                column: "FunctionalCategoriesId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDescription_FunctionalClassId",
                table: "JobDescription",
                column: "FunctionalClassId");

            migrationBuilder.CreateIndex(
                name: "IX_JobDescription_JobRanksId",
                table: "JobDescription",
                column: "JobRanksId");

            migrationBuilder.CreateIndex(
                name: "IX_linkingEmployeesToShiftPeriods_DepartmentsId",
                table: "linkingEmployeesToShiftPeriods",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_linkingEmployeesToShiftPeriods_EmployeeId",
                table: "linkingEmployeesToShiftPeriods",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_linkingEmployeesToShiftPeriods_PeriodsId",
                table: "linkingEmployeesToShiftPeriods",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_linkingEmployeesToShiftPeriods_PermanenceModelsId",
                table: "linkingEmployeesToShiftPeriods",
                column: "PermanenceModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_linkingEmployeesToShiftPeriods_SectionsId",
                table: "linkingEmployeesToShiftPeriods",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_DepartmentId",
                table: "MachineInfo",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_SectionId",
                table: "MachineInfo",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_oneFingerprints_EmployeeId",
                table: "oneFingerprints",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_openingBalancesForVacations_EmployeeId",
                table: "openingBalancesForVacations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_openingBalancesForVacations_PublicHolidaysId",
                table: "openingBalancesForVacations",
                column: "PublicHolidaysId");

            migrationBuilder.CreateIndex(
                name: "IX_Penalties_PenaltiesAndViolationsFormsId",
                table: "Penalties",
                column: "PenaltiesAndViolationsFormsId");

            migrationBuilder.CreateIndex(
                name: "IX_penaltiesAndViolationsForms_PenaltiesId",
                table: "penaltiesAndViolationsForms",
                column: "PenaltiesId");

            migrationBuilder.CreateIndex(
                name: "IX_penaltiesAndViolationsForms_ViolationsId",
                table: "penaltiesAndViolationsForms",
                column: "ViolationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_PeriodsId",
                table: "Periods",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_PermanenceModelsId",
                table: "Periods",
                column: "PermanenceModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_Permits_EmployeeId",
                table: "Permits",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_personalDatas_EmployeeId",
                table: "personalDatas",
                column: "EmployeeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personalDatas_GuaranteesId",
                table: "personalDatas",
                column: "GuaranteesId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_personalDatas_MaritalStatusId",
                table: "personalDatas",
                column: "MaritalStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_personalDatas_NationalityId",
                table: "personalDatas",
                column: "NationalityId");

            migrationBuilder.CreateIndex(
                name: "IX_personalDatas_ReligionId",
                table: "personalDatas",
                column: "ReligionId");

            migrationBuilder.CreateIndex(
                name: "IX_personalDatas_SexId",
                table: "personalDatas",
                column: "SexId");

            migrationBuilder.CreateIndex(
                name: "IX_practicalExperiences_EmployeeId",
                table: "practicalExperiences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_DepartmentsId",
                table: "Sections",
                column: "DepartmentsId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionsAccounts_FinanceAccountId",
                table: "SectionsAccounts",
                column: "FinanceAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionsAccounts_FinanceAccountTypeId",
                table: "SectionsAccounts",
                column: "FinanceAccountTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_SectionsAccounts_SectionsId",
                table: "SectionsAccounts",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_sectors_BranchesId",
                table: "sectors",
                column: "BranchesId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecialtiesAndQualification_qualificationsId",
                table: "SpecialtiesAndQualification",
                column: "qualificationsId");

            migrationBuilder.CreateIndex(
                name: "IX_staffTimes_EmployeeId",
                table: "staffTimes",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_staffTimes_PeriodId",
                table: "staffTimes",
                column: "PeriodId");

            migrationBuilder.CreateIndex(
                name: "IX_staffTimes_PermanenceModelsId",
                table: "staffTimes",
                column: "PermanenceModelsId");

            migrationBuilder.CreateIndex(
                name: "IX_staffTimes_SectionsId",
                table: "staffTimes",
                column: "SectionsId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffVacations_EmployeeId",
                table: "StaffVacations",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffVacations_PeriodsId",
                table: "StaffVacations",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffVacations_SectionId",
                table: "StaffVacations",
                column: "SectionId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffVacations_SubstituteStaffMemberId",
                table: "StaffVacations",
                column: "SubstituteStaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffVacations_VacationId",
                table: "StaffVacations",
                column: "VacationId");

            migrationBuilder.CreateIndex(
                name: "IX_statementOfEmployeeFiles_EmployeeId",
                table: "statementOfEmployeeFiles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_trainingCourses_EmployeeId",
                table: "trainingCourses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversitiesAndQualification_universitiesId",
                table: "UniversitiesAndQualification",
                column: "universitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationAllowances_EmployeeId",
                table: "VacationAllowances",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_VacationBalance_EmployeeId",
                table: "VacationBalance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_weekends_PeriodsId",
                table: "weekends",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_weekends_PermanenceModelsId",
                table: "weekends",
                column: "PermanenceModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeViolations_Penalties_PenaltiesId",
                table: "EmployeeViolations",
                column: "PenaltiesId",
                principalTable: "Penalties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Penalties_penaltiesAndViolationsForms_PenaltiesAndViolationsFormsId",
                table: "Penalties",
                column: "PenaltiesAndViolationsFormsId",
                principalTable: "penaltiesAndViolationsForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_penaltiesAndViolationsForms_Penalties_PenaltiesId",
                table: "penaltiesAndViolationsForms");

            migrationBuilder.DropTable(
                name: "additionalAccountInformation");

            migrationBuilder.DropTable(
                name: "AdditionalExternalOfWork");

            migrationBuilder.DropTable(
                name: "AdditionalUnsupportedEmployees");

            migrationBuilder.DropTable(
                name: "adjustingTimes");

            migrationBuilder.DropTable(
                name: "AdministrativeDecisions");

            migrationBuilder.DropTable(
                name: "AdministrativePromotions");

            migrationBuilder.DropTable(
                name: "allowancesAndDiscounts");

            migrationBuilder.DropTable(
                name: "AnnualGoals");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AttendanceAndAbsenceProcessing");

            migrationBuilder.DropTable(
                name: "AttendanceLog");

            migrationBuilder.DropTable(
                name: "AttendanceRecord");

            migrationBuilder.DropTable(
                name: "AutomaticallyApprovedAdd_on");

            migrationBuilder.DropTable(
                name: "basicDataForTheSalaryStatements");

            migrationBuilder.DropTable(
                name: "basicDataForWagesAndSalaries");

            migrationBuilder.DropTable(
                name: "contractTerms");

            migrationBuilder.DropTable(
                name: "EducationalQualificationAndQualification");

            migrationBuilder.DropTable(
                name: "EmployeeAdvances");

            migrationBuilder.DropTable(
                name: "EmployeeArchives");

            migrationBuilder.DropTable(
                name: "EmployeeLoans");

            migrationBuilder.DropTable(
                name: "EmployeeMovements");

            migrationBuilder.DropTable(
                name: "EmployeePerks");

            migrationBuilder.DropTable(
                name: "EmployeePermissions");

            migrationBuilder.DropTable(
                name: "EmployeesQualifications");

            migrationBuilder.DropTable(
                name: "EmployeeViolations");

            migrationBuilder.DropTable(
                name: "EmploymentStatusManagement");

            migrationBuilder.DropTable(
                name: "EndOfServiceClearance");

            migrationBuilder.DropTable(
                name: "EntitlementsAndDeductions");

            migrationBuilder.DropTable(
                name: "Family");

            migrationBuilder.DropTable(
                name: "financialStatements");

            migrationBuilder.DropTable(
                name: "FunctionalFilesOfStatementOfEmployeeFiles");

            migrationBuilder.DropTable(
                name: "linkingEmployeesToShiftPeriods");

            migrationBuilder.DropTable(
                name: "MachineInfo");

            migrationBuilder.DropTable(
                name: "months");

            migrationBuilder.DropTable(
                name: "officialVacations");

            migrationBuilder.DropTable(
                name: "oneFingerprints");

            migrationBuilder.DropTable(
                name: "openingBalancesForVacations");

            migrationBuilder.DropTable(
                name: "permissionToAttendAndLeaves");

            migrationBuilder.DropTable(
                name: "Permits");

            migrationBuilder.DropTable(
                name: "personalDatas");

            migrationBuilder.DropTable(
                name: "practicalExperiences");

            migrationBuilder.DropTable(
                name: "publicAdministrations");

            migrationBuilder.DropTable(
                name: "SectionsAccounts");

            migrationBuilder.DropTable(
                name: "SpecialtiesAndQualification");

            migrationBuilder.DropTable(
                name: "staffTimes");

            migrationBuilder.DropTable(
                name: "StaffVacations");

            migrationBuilder.DropTable(
                name: "trainingCourses");

            migrationBuilder.DropTable(
                name: "UniversitiesAndQualification");

            migrationBuilder.DropTable(
                name: "VacationAllowances");

            migrationBuilder.DropTable(
                name: "VacationBalance");

            migrationBuilder.DropTable(
                name: "weekends");

            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "AttendanceStatus");

            migrationBuilder.DropTable(
                name: "contracts");

            migrationBuilder.DropTable(
                name: "educationalQualifications");

            migrationBuilder.DropTable(
                name: "EmployeeAccount");

            migrationBuilder.DropTable(
                name: "permissions");

            migrationBuilder.DropTable(
                name: "relativesTypes");

            migrationBuilder.DropTable(
                name: "functionalFiles");

            migrationBuilder.DropTable(
                name: "statementOfEmployeeFiles");

            migrationBuilder.DropTable(
                name: "guarantees");

            migrationBuilder.DropTable(
                name: "nationality");

            migrationBuilder.DropTable(
                name: "religion");

            migrationBuilder.DropTable(
                name: "sex");

            migrationBuilder.DropTable(
                name: "specialties");

            migrationBuilder.DropTable(
                name: "publicHolidays");

            migrationBuilder.DropTable(
                name: "Universities");

            migrationBuilder.DropTable(
                name: "qualifications");

            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.DropTable(
                name: "FinanceAccountType");

            migrationBuilder.DropTable(
                name: "FinanceAccount");

            migrationBuilder.DropTable(
                name: "employee");

            migrationBuilder.DropTable(
                name: "maritalStatuses");

            migrationBuilder.DropTable(
                name: "permanenceModels");

            migrationBuilder.DropTable(
                name: "JobDescription");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropTable(
                name: "fingerprintDevices");

            migrationBuilder.DropTable(
                name: "functionalCategories");

            migrationBuilder.DropTable(
                name: "functionalClasses");

            migrationBuilder.DropTable(
                name: "jobRanks");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropTable(
                name: "sectors");

            migrationBuilder.DropTable(
                name: "branches");

            migrationBuilder.DropTable(
                name: "company");

            migrationBuilder.DropTable(
                name: "directorates");

            migrationBuilder.DropTable(
                name: "boardOfDirectors");

            migrationBuilder.DropTable(
                name: "governorates");

            migrationBuilder.DropTable(
                name: "membershipOfTheBoardOfs");

            migrationBuilder.DropTable(
                name: "country");

            migrationBuilder.DropTable(
                name: "Penalties");

            migrationBuilder.DropTable(
                name: "penaltiesAndViolationsForms");

            migrationBuilder.DropTable(
                name: "Violations");
        }
    }
}
