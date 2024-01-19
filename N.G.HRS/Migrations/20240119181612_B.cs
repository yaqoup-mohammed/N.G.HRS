using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class B : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_Sections_SectionsId",
                table: "staffTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_permanenceModels_PermanenceModelsId",
                table: "staffTimes");

            migrationBuilder.AlterColumn<int>(
                name: "SectionsId",
                table: "staffTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PermanenceModelsId",
                table: "staffTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "staffTimes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "HoursOfWorks",
                table: "permanenceModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EarlyDeparturePermission",
                table: "permanenceModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<int>(
                name: "AllowanceForLateAttendance",
                table: "permanenceModels",
                type: "int",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AddColumn<bool>(
                name: "Closed",
                table: "months",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PeriodsId",
                table: "adjustingTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PermanenceModelsId",
                table: "adjustingTimes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_adjustingTimes_PeriodsId",
                table: "adjustingTimes",
                column: "PeriodsId");

            migrationBuilder.CreateIndex(
                name: "IX_adjustingTimes_PermanenceModelsId",
                table: "adjustingTimes",
                column: "PermanenceModelsId");

            migrationBuilder.AddForeignKey(
                name: "FK_adjustingTimes_periods_PeriodsId",
                table: "adjustingTimes",
                column: "PeriodsId",
                principalTable: "periods",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_adjustingTimes_permanenceModels_PermanenceModelsId",
                table: "adjustingTimes",
                column: "PermanenceModelsId",
                principalTable: "permanenceModels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_staffTimes_Sections_SectionsId",
                table: "staffTimes",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_staffTimes_permanenceModels_PermanenceModelsId",
                table: "staffTimes",
                column: "PermanenceModelsId",
                principalTable: "permanenceModels",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_adjustingTimes_periods_PeriodsId",
                table: "adjustingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_adjustingTimes_permanenceModels_PermanenceModelsId",
                table: "adjustingTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_Sections_SectionsId",
                table: "staffTimes");

            migrationBuilder.DropForeignKey(
                name: "FK_staffTimes_permanenceModels_PermanenceModelsId",
                table: "staffTimes");

            migrationBuilder.DropIndex(
                name: "IX_adjustingTimes_PeriodsId",
                table: "adjustingTimes");

            migrationBuilder.DropIndex(
                name: "IX_adjustingTimes_PermanenceModelsId",
                table: "adjustingTimes");

            migrationBuilder.DropColumn(
                name: "Closed",
                table: "months");

            migrationBuilder.DropColumn(
                name: "PeriodsId",
                table: "adjustingTimes");

            migrationBuilder.DropColumn(
                name: "PermanenceModelsId",
                table: "adjustingTimes");

            migrationBuilder.AlterColumn<int>(
                name: "SectionsId",
                table: "staffTimes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PermanenceModelsId",
                table: "staffTimes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "staffTimes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HoursOfWorks",
                table: "permanenceModels",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "EarlyDeparturePermission",
                table: "permanenceModels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "AllowanceForLateAttendance",
                table: "permanenceModels",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_staffTimes_Sections_SectionsId",
                table: "staffTimes",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_staffTimes_permanenceModels_PermanenceModelsId",
                table: "staffTimes",
                column: "PermanenceModelsId",
                principalTable: "permanenceModels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
