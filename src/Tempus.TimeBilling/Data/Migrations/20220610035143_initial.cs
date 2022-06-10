using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.TimeBilling.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Tempus");

            migrationBuilder.CreateTable(
                name: "TimeBills",
                schema: "Tempus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    WorkerId = table.Column<int>(type: "int", nullable: false),
                    WorkPerformed = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: true),
                    Rate = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    Hours = table.Column<decimal>(type: "decimal(8,1)", precision: 8, scale: 1, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeBills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeBills",
                schema: "Tempus");
        }
    }
}
