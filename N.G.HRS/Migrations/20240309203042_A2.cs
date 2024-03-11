using System;
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
                name: "FK_adjustingTimes_periods_PeriodsId",
                table: "adjustingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_linkingEmployeesToShiftPeriods_periods_PeriodsId",
                table: "linkingEmployeesToShiftPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_weekends_periods_PeriodsId",
                table: "weekends");

            migrationBuilder.DropTable(
                name: "Attendance");

            migrationBuilder.DropPrimaryKey(
                name: "PK_periods",
                table: "periods");

            migrationBuilder.DropColumn(
                name: "AddAttendanceAndDeparturePermission",
                table: "permanenceModels");

            migrationBuilder.DropColumn(
                name: "AllowanceForLateAttendance",
                table: "permanenceModels");

            migrationBuilder.DropColumn(
                name: "EarlyDeparturePermission",
                table: "permanenceModels");

            migrationBuilder.RenameTable(
                name: "periods",
                newName: "Periods");

            migrationBuilder.RenameColumn(
                name: "WorkBetweenTwoDays",
                table: "permanenceModels",
                newName: "WorkBetweenTwoShifts");

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "Periods",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Muinutes",
                table: "Periods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeriodsId",
                table: "Periods",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PermanenceModelsId",
                table: "Periods",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Periods",
                table: "Periods",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_PeriodsId",
                table: "Periods",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_Periods_PermanenceModelsId",
                table: "Periods",
                column: "PermanenceModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_adjustingTimes_Periods_PeriodsId",
                table: "adjustingTimes",
                column: "PeriodsId",
                principalTable: "Periods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_linkingEmployeesToShiftPeriods_Periods_PeriodsId",
                table: "linkingEmployeesToShiftPeriods",
                column: "PeriodsId",
                principalTable: "Periods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Periods_Periods_PeriodsId",
                table: "Periods",
                column: "PeriodsId",
                principalTable: "Periods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Periods_permanenceModels_PermanenceModelsId",
                table: "Periods",
                column: "PermanenceModelsId",
                principalTable: "permanenceModels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_weekends_Periods_PeriodsId",
                table: "weekends",
                column: "PeriodsId",
                principalTable: "Periods",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adjustingTimes_Periods_PeriodsId",
                table: "adjustingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_linkingEmployeesToShiftPeriods_Periods_PeriodsId",
                table: "linkingEmployeesToShiftPeriods");

            migrationBuilder.DropForeignKey(
                name: "FK_Periods_Periods_PeriodsId",
                table: "Periods");

            migrationBuilder.DropForeignKey(
                name: "FK_Periods_permanenceModels_PermanenceModelsId",
                table: "Periods");

            migrationBuilder.DropForeignKey(
                name: "FK_weekends_Periods_PeriodsId",
                table: "weekends");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Periods",
                table: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Periods_PeriodsId",
                table: "Periods");

            migrationBuilder.DropIndex(
                name: "IX_Periods_PermanenceModelsId",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "Muinutes",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "PeriodsId",
                table: "Periods");

            migrationBuilder.DropColumn(
                name: "PermanenceModelsId",
                table: "Periods");

            migrationBuilder.RenameTable(
                name: "Periods",
                newName: "periods");

            migrationBuilder.RenameColumn(
                name: "WorkBetweenTwoShifts",
                table: "permanenceModels",
                newName: "WorkBetweenTwoDays");

            migrationBuilder.AddColumn<bool>(
                name: "AddAttendanceAndDeparturePermission",
                table: "permanenceModels",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "AllowanceForLateAttendance",
                table: "permanenceModels",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "EarlyDeparturePermission",
                table: "permanenceModels",
                type: "float",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Hours",
                table: "periods",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_periods",
                table: "periods",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Attendance",
                columns: table => new
                {
                    AttendanceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    dwInOutMode = table.Column<int>(type: "int", nullable: false),
                    dwVerifyMode = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.AttendanceId);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_adjustingTimes_periods_PeriodsId",
                table: "adjustingTimes",
                column: "PeriodsId",
                principalTable: "periods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_linkingEmployeesToShiftPeriods_periods_PeriodsId",
                table: "linkingEmployeesToShiftPeriods",
                column: "PeriodsId",
                principalTable: "periods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_weekends_periods_PeriodsId",
                table: "weekends",
                column: "PeriodsId",
                principalTable: "periods",
                principalColumn: "Id");
        }
    }
}
