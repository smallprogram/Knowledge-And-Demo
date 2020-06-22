using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTfulApi.Api.Migrations
{
    public partial class AddEmployeeData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                column: "Gender",
                value: 2);

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("aee28ab3-146f-4036-b417-decb89483cff"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT225", "Mande", 1, "Etfb" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("a9e0b740-4f88-470f-88dc-7f5b1f72d8cc"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT345", "Zhu", 1, "Sir" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("2c96ef7c-457c-4722-8b17-5d37240f5eaf"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT545", "Lv", 2, "Xiaolu" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("0c781f03-b82f-45b2-9427-203e47ee47ed"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT145", "Anne", 2, "Dei" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("0c781f03-b82f-45b2-9427-203e47ee47ed"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("2c96ef7c-457c-4722-8b17-5d37240f5eaf"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("a9e0b740-4f88-470f-88dc-7f5b1f72d8cc"));

            migrationBuilder.DeleteData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("aee28ab3-146f-4036-b417-decb89483cff"));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"),
                column: "Gender",
                value: 1);
        }
    }
}
