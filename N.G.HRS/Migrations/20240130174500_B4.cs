using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class B4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contractTerms_contracts_ContractsId",
                table: "contractTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_Departments_employee_EmployeeId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_practicalExperiences_employee_EmployeeId",
                table: "practicalExperiences");

            migrationBuilder.DropForeignKey(
                name: "FK_statementOfEmployeeFiles_employee_EmployeeId",
                table: "statementOfEmployeeFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_trainingCourses_employee_EmployeeId",
                table: "trainingCourses");

            migrationBuilder.DropIndex(
                name: "IX_trainingCourses_EmployeeId",
                table: "trainingCourses");

            migrationBuilder.DropIndex(
                name: "IX_statementOfEmployeeFiles_EmployeeId",
                table: "statementOfEmployeeFiles");

            migrationBuilder.DropIndex(
                name: "IX_practicalExperiences_EmployeeId",
                table: "practicalExperiences");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "trainingCourses");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "statementOfEmployeeFiles");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "practicalExperiences");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "PracticalExperiencesId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatementOfEmployeeFilesId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TrainingCoursesId",
                table: "employee",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractsId",
                table: "contractTerms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_employee_PracticalExperiencesId",
                table: "employee",
                column: "PracticalExperiencesId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_StatementOfEmployeeFilesId",
                table: "employee",
                column: "StatementOfEmployeeFilesId");

            migrationBuilder.CreateIndex(
                name: "IX_employee_TrainingCoursesId",
                table: "employee",
                column: "TrainingCoursesId");

            migrationBuilder.AddForeignKey(
                name: "FK_contractTerms_contracts_ContractsId",
                table: "contractTerms",
                column: "ContractsId",
                principalTable: "contracts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_practicalExperiences_PracticalExperiencesId",
                table: "employee",
                column: "PracticalExperiencesId",
                principalTable: "practicalExperiences",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_statementOfEmployeeFiles_StatementOfEmployeeFilesId",
                table: "employee",
                column: "StatementOfEmployeeFilesId",
                principalTable: "statementOfEmployeeFiles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_employee_trainingCourses_TrainingCoursesId",
                table: "employee",
                column: "TrainingCoursesId",
                principalTable: "trainingCourses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_contractTerms_contracts_ContractsId",
                table: "contractTerms");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_practicalExperiences_PracticalExperiencesId",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_statementOfEmployeeFiles_StatementOfEmployeeFilesId",
                table: "employee");

            migrationBuilder.DropForeignKey(
                name: "FK_employee_trainingCourses_TrainingCoursesId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_PracticalExperiencesId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_StatementOfEmployeeFilesId",
                table: "employee");

            migrationBuilder.DropIndex(
                name: "IX_employee_TrainingCoursesId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "PracticalExperiencesId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "StatementOfEmployeeFilesId",
                table: "employee");

            migrationBuilder.DropColumn(
                name: "TrainingCoursesId",
                table: "employee");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "trainingCourses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "statementOfEmployeeFiles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "practicalExperiences",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ContractsId",
                table: "contractTerms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_trainingCourses_EmployeeId",
                table: "trainingCourses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_statementOfEmployeeFiles_EmployeeId",
                table: "statementOfEmployeeFiles",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_practicalExperiences_EmployeeId",
                table: "practicalExperiences",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_contractTerms_contracts_ContractsId",
                table: "contractTerms",
                column: "ContractsId",
                principalTable: "contracts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_employee_EmployeeId",
                table: "Departments",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_practicalExperiences_employee_EmployeeId",
                table: "practicalExperiences",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_statementOfEmployeeFiles_employee_EmployeeId",
                table: "statementOfEmployeeFiles",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_trainingCourses_employee_EmployeeId",
                table: "trainingCourses",
                column: "EmployeeId",
                principalTable: "employee",
                principalColumn: "Id");
        }
    }
}
