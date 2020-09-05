using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EmployeeManagement.Api.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<Guid>(nullable: false),
                    DepartmentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    DateOfBrith = table.Column<DateTime>(nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<Guid>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "DepartmentId", "DepartmentName" },
                values: new object[,]
                {
                    { new Guid("cbcebfdc-d176-4045-a680-75d5893fe185"), "IT" },
                    { new Guid("fa87c5b3-0bbb-4ddb-a4ee-33a7a875cfe9"), "HR" },
                    { new Guid("53049207-ed06-4411-b920-9ab7de6b5c0e"), "Payroll" },
                    { new Guid("8347d0d5-5087-44a9-b3e6-1fcbb2d399dc"), "Admin" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "EmployeeId", "DateOfBrith", "DepartmentId", "Email", "FirstName", "Gender", "LastName", "PhotoPath" },
                values: new object[,]
                {
                    { new Guid("e5122517-92e7-42da-a445-915de6ee9717"), new DateTime(1980, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cbcebfdc-d176-4045-a680-75d5893fe185"), "David@pragimtech.com", "John", 0, "Hastings", "images/john.jpg" },
                    { new Guid("72b544d4-b703-4e43-baa9-25612d6dd7bf"), new DateTime(1981, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("fa87c5b3-0bbb-4ddb-a4ee-33a7a875cfe9"), "Sam@pragimtech.com", "Sam", 0, "Galloway", "images/sam.jpg" },
                    { new Guid("6a39fe40-8a48-492f-b5af-de38a2946b91"), new DateTime(1979, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("cbcebfdc-d176-4045-a680-75d5893fe185"), "mary@pragimtech.com", "Mary", 1, "Smith", "images/mary.jpg" },
                    { new Guid("b15ace62-d94d-40fa-8e22-f048998ded80"), new DateTime(1982, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new Guid("53049207-ed06-4411-b920-9ab7de6b5c0e"), "sara@pragimtech.com", "Sara", 1, "Longway", "images/sara.jpg" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Employees");
        }
    }
}
