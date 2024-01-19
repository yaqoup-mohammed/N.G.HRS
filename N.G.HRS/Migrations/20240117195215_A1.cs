using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personalDatas_nationality_NationalityId",
                table: "personalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_personalDatas_religion_ReligionId",
                table: "personalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_personalDatas_sex_SexId",
                table: "personalDatas");

            migrationBuilder.AlterColumn<int>(
                name: "SexId",
                table: "personalDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "personalDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NationalityId",
                table: "personalDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "personalDatas",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_personalDatas_nationality_NationalityId",
                table: "personalDatas",
                column: "NationalityId",
                principalTable: "nationality",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_personalDatas_religion_ReligionId",
                table: "personalDatas",
                column: "ReligionId",
                principalTable: "religion",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_personalDatas_sex_SexId",
                table: "personalDatas",
                column: "SexId",
                principalTable: "sex",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_personalDatas_nationality_NationalityId",
                table: "personalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_personalDatas_religion_ReligionId",
                table: "personalDatas");

            migrationBuilder.DropForeignKey(
                name: "FK_personalDatas_sex_SexId",
                table: "personalDatas");

            migrationBuilder.AlterColumn<int>(
                name: "SexId",
                table: "personalDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ReligionId",
                table: "personalDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NationalityId",
                table: "personalDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatusId",
                table: "personalDatas",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_personalDatas_nationality_NationalityId",
                table: "personalDatas",
                column: "NationalityId",
                principalTable: "nationality",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personalDatas_religion_ReligionId",
                table: "personalDatas",
                column: "ReligionId",
                principalTable: "religion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_personalDatas_sex_SexId",
                table: "personalDatas",
                column: "SexId",
                principalTable: "sex",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
