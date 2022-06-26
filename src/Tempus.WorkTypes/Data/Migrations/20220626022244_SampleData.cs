using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.WorkTypes.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "Tempus",
                table: "WorkTypes",
                columns: new[] { "Id", "DefaultRate", "Description", "Name" },
                values: new object[] { 1, 100m, "Discussions with client.", "Consulting" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "Tempus",
                table: "WorkTypes",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
