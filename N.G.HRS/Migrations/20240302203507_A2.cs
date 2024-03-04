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
                name: "FK_AutomaticallyApprovedAdd_on_Departments_DepartmentId",
                table: "AutomaticallyApprovedAdd_on");

            migrationBuilder.RenameColumn(
                name: "DepartmentId",
                table: "AutomaticallyApprovedAdd_on",
                newName: "SectionsId");

            migrationBuilder.RenameIndex(
                name: "IX_AutomaticallyApprovedAdd_on_DepartmentId",
                table: "AutomaticallyApprovedAdd_on",
                newName: "IX_AutomaticallyApprovedAdd_on_SectionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                column: "SectionsId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Sections_SectionsId",
                table: "AutomaticallyApprovedAdd_on");

            migrationBuilder.RenameColumn(
                name: "SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                newName: "DepartmentId");

            migrationBuilder.RenameIndex(
                name: "IX_AutomaticallyApprovedAdd_on_SectionsId",
                table: "AutomaticallyApprovedAdd_on",
                newName: "IX_AutomaticallyApprovedAdd_on_DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AutomaticallyApprovedAdd_on_Departments_DepartmentId",
                table: "AutomaticallyApprovedAdd_on",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
