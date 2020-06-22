using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTfulApi.Api.Migrations
{
    public partial class AddEmployeeData2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("0c781f03-b82f-45b2-9427-203e47ee47ed"),
                column: "DateOfBirth",
                value: new DateTime(1983, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("2c96ef7c-457c-4722-8b17-5d37240f5eaf"),
                column: "DateOfBirth",
                value: new DateTime(1989, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("72457e73-ea34-4e02-b575-8d384e82a481"),
                column: "DateOfBirth",
                value: new DateTime(1997, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("a9e0b740-4f88-470f-88dc-7f5b1f72d8cc"),
                column: "DateOfBirth",
                value: new DateTime(1989, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("aee28ab3-146f-4036-b417-decb89483cff"),
                column: "DateOfBirth",
                value: new DateTime(1990, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("0c781f03-b82f-45b2-9427-203e47ee47ed"),
                column: "DateOfBirth",
                value: new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("2c96ef7c-457c-4722-8b17-5d37240f5eaf"),
                column: "DateOfBirth",
                value: new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("72457e73-ea34-4e02-b575-8d384e82a481"),
                column: "DateOfBirth",
                value: new DateTime(1986, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("a9e0b740-4f88-470f-88dc-7f5b1f72d8cc"),
                column: "DateOfBirth",
                value: new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Employees",
                keyColumn: "Id",
                keyValue: new Guid("aee28ab3-146f-4036-b417-decb89483cff"),
                column: "DateOfBirth",
                value: new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
