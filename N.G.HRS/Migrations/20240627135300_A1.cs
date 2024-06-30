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
            migrationBuilder.RenameColumn(
                name: "Section",
                table: "MachineInfo",
                newName: "SectionId");

            migrationBuilder.RenameColumn(
                name: "Department",
                table: "MachineInfo",
                newName: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_DepartmentId",
                table: "MachineInfo",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineInfo_SectionId",
                table: "MachineInfo",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_MachineInfo_Departments_DepartmentId",
                table: "MachineInfo",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MachineInfo_Sections_SectionId",
                table: "MachineInfo",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MachineInfo_Departments_DepartmentId",
                table: "MachineInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_MachineInfo_Sections_SectionId",
                table: "MachineInfo");

            migrationBuilder.DropIndex(
                name: "IX_MachineInfo_DepartmentId",
                table: "MachineInfo");

            migrationBuilder.DropIndex(
                name: "IX_MachineInfo_SectionId",
                table: "MachineInfo");

            migrationBuilder.RenameColumn(
                name: "SectionId",
                table: "MachineInfo",
                newName: "Section");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "MachineInfo",
                newName: "Department");
        }
    }
}
