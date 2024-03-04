using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A12 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                table: "AutomaticallyApprovedAdd_on");

            migrationBuilder.AlterColumn<int>(
                name: "SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AutomaticallyApprovedAdd_on",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                table: "AutomaticallyApprovedAdd_on");

            migrationBuilder.AlterColumn<int>(
                name: "SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "AutomaticallyApprovedAdd_on",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
