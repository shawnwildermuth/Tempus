using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Customers.Migrations
{
    public partial class initialdb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Tempus");

            migrationBuilder.CreateTable(
                name: "Customers",
                schema: "Tempus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CompanyPhone = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "Tempus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => new { x.CustomerId, x.Id });
                    table.ForeignKey(
                        name: "FK_Contacts_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Tempus",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "Tempus",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    LineOne = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LineTwo = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    LineThree = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StateProvince = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.CustomerId);
                    table.ForeignKey(
                        name: "FK_Locations_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalSchema: "Tempus",
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Tempus",
                table: "Customers",
                columns: new[] { "Id", "CompanyName", "CompanyPhone" },
                values: new object[] { 1, "Smith Cleaning", "(404) 555-2121" });

            migrationBuilder.InsertData(
                schema: "Tempus",
                table: "Contacts",
                columns: new[] { "CustomerId", "Id", "Email", "FirstName", "LastName", "MiddleName", "Phone", "Title" },
                values: new object[] { 1, 1, null, "Sarah", "Smith", null, null, "President" });

            migrationBuilder.InsertData(
                schema: "Tempus",
                table: "Locations",
                columns: new[] { "CustomerId", "City", "Country", "Id", "LineOne", "LineThree", "LineTwo", "PostalCode", "StateProvince" },
                values: new object[] { 1, "Austell", "USA", 1, "123 Main Street", null, "Suite 400", "30303", "GA" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "Tempus");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "Tempus");

            migrationBuilder.DropTable(
                name: "Customers",
                schema: "Tempus");
        }
    }
}
