using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RESTFul.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Companies",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Country = table.Column<string>(maxLength: 50, nullable: true),
                    Industry = table.Column<string>(maxLength: 50, nullable: true),
                    Product = table.Column<string>(maxLength: 100, nullable: true),
                    Introduction = table.Column<string>(maxLength: 500, nullable: false),
                    BankruptTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CompanyId = table.Column<Guid>(nullable: false),
                    EmployeeNo = table.Column<string>(maxLength: 10, nullable: false),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    Gender = table.Column<int>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Software", "Great Company", "Microsoft", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237144"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Internet", "Not Exists?", "AOL", "Website" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542833"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "ECommerce", "Store", "Amazon", "Books" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716433"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "Internet", "Music?", "NetEase", "Songs" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237133"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "ECommerce", "Brothers", "Jingdong", "Goods" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542822"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "Security", "- -", "360", "Security Product" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716422"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Internet", "Blocked", "Youtube", "Videos" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237122"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Internet", "Blocked", "Twitter", "Tweets" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542811"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "ECommerce", "From Jiangsu", "Suning", "Goods" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716444"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Internet", "Who?", "Yahoo", "Mail" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716411"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Italy", "Football", "Football Club", "AC Milan", "Football Match" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542800"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Software", "Photoshop?", "Adobe", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716400"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "Internet", "From Beijing", "Baidu", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237100"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "ECommerce", "From Shenzhen", "Tencent", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542853"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "Internet", "Fubao Company", "Alipapa", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716440"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Internet", "Don't be evil", "Google", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df5923716c"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Software", "Great Company", "Microsoft", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("a2f92442-73dc-4091-ad4d-4398b39f4d47"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "China", "Software", "FuBao Company", "Alibaba", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("e2b07120-ce3d-4e30-8fa8-a4fb76c663e5"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Software", "Don't be evil", "Google", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237111"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Technology", "Wow", "SpaceX", "Rocket" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "BankruptTime", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542844"), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Internet", "Is it a company?", "Firefox", "Browser" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("4b501cb3-d168-4cc0-b375-48fb33f318a4"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1976, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT231", "Nick", 1, "Carter" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7eaa532c-1be5-472c-a738-94fd26e5fad6"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1981, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT245", "Vince", 2, "Carter" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("aee28ab3-146f-4036-b417-decb89483cff"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1990, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT225", "Mande", 1, "Etfb" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("a9e0b740-4f88-470f-88dc-7f5b1f72d8cc"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1989, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT345", "Zhu", 1, "Sir" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("2c96ef7c-457c-4722-8b17-5d37240f5eaf"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1989, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT545", "Lv", 2, "Xiaolu" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("0c781f03-b82f-45b2-9427-203e47ee47ed"), new Guid("e2f039ad-237c-4efe-97e9-15deccda6691"), new DateTime(1983, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "MSFT145", "Anne", 2, "Dei" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("72457e73-ea34-4e02-b575-8d384e82a481"), new Guid("e2b07120-ce3d-4e30-8fa8-a4fb76c663e5"), new DateTime(1997, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "G003", "Mary", 2, "King" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("7644b71d-d74e-43e2-ac32-8cbadd7b1c3a"), new Guid("e2b07120-ce3d-4e30-8fa8-a4fb76c663e5"), new DateTime(1977, 4, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "G097", "Kevin", 1, "Richardson" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("679dfd33-32e4-4393-b061-f7abb8956f53"), new Guid("a2f92442-73dc-4091-ad4d-4398b39f4d47"), new DateTime(1967, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "A009", "卡", 2, "里" });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CompanyId", "DateOfBirth", "EmployeeNo", "FirstName", "Gender", "LastName" },
                values: new object[] { new Guid("1861341e-b42b-410c-ae21-cf11f36fc574"), new Guid("a2f92442-73dc-4091-ad4d-4398b39f4d47"), new DateTime(1957, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "A404", "Not", 1, "Man" });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CompanyId",
                table: "Employees",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Companies");
        }
    }
}
