using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace N.G.HRS.Migrations
{
    /// <inheritdoc />
    public partial class A0 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Assignment",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "AttendanceStatus",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
