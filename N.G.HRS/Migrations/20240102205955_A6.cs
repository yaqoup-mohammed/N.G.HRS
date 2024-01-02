using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentAccountsId",
                table: "Sections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "FinanceAccount",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
                    DepartmentAccountsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceAccount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceAccount_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceAccount_departmentAccounts_DepartmentAccountsId",
                        column: x => x.DepartmentAccountsId,
                        principalTable: "departmentAccounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeAccountId = table.Column<int>(type: "int", nullable: false),
                    DepartmentAccountsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinanceAccountType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FinanceAccountType_EmployeeAccount_EmployeeAccountId",
                        column: x => x.EmployeeAccountId,
                        principalTable: "EmployeeAccount",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FinanceAccountType_departmentAccounts_DepartmentAccountsId",
                        column: x => x.DepartmentAccountsId,
                        principalTable: "departmentAccounts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sections_DepartmentAccountsId",
                table: "Sections",
                column: "DepartmentAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccount_DepartmentAccountsId",
                table: "FinanceAccount",
                column: "DepartmentAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccount_EmployeeAccountId",
                table: "FinanceAccount",
                column: "EmployeeAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccountType_DepartmentAccountsId",
                table: "FinanceAccountType",
                column: "DepartmentAccountsId");

            migrationBuilder.CreateIndex(
                name: "IX_FinanceAccountType_EmployeeAccountId",
                table: "FinanceAccountType",
                column: "EmployeeAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_departmentAccounts_DepartmentAccountsId",
                table: "Sections",
                column: "DepartmentAccountsId",
                principalTable: "departmentAccounts",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_departmentAccounts_DepartmentAccountsId",
                table: "Sections");

            migrationBuilder.DropTable(
                name: "FinanceAccount");

            migrationBuilder.DropTable(
                name: "FinanceAccountType");

            migrationBuilder.DropIndex(
                name: "IX_Sections_DepartmentAccountsId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "DepartmentAccountsId",
                table: "Sections");
        }
    }
}
