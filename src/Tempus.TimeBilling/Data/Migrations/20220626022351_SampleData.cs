using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.TimeBilling.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Tempus",
                table: "TimeBills",
                columns: new[] { "Id", "CustomerId", "Hours", "Notes", "Rate", "WorkPerformed", "WorkerId" },
                values: new object[] { 1, 1, 1.3m, "Worked with the client on architecture.", 135m, "Consulting", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Tempus",
                table: "TimeBills",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
