using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Workers.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Tempus",
                table: "Workers",
                columns: new[] { "Id", "BaseRate", "Email", "FirstName", "LastName", "Phone", "UserName" },
                values: new object[] { 1, 150m, "shawn@aol.com", "Shawn", "Wildermuth", "(404) 555-1212", "shawnwildermuth" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Tempus",
                table: "Workers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
