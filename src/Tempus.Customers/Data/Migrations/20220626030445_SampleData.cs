using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tempus.Customers.Migrations
{
    public partial class SampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Locations_LocationId",
                schema: "Tempus",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                schema: "Tempus",
                table: "Locations");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LocationId",
                schema: "Tempus",
                table: "Customers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                schema: "Tempus",
                table: "Contacts");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_CustomerId",
                schema: "Tempus",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                schema: "Tempus",
                table: "Customers");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tempus",
                table: "Locations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                schema: "Tempus",
                table: "Locations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Tempus",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "Tempus",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Tempus",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                schema: "Tempus",
                table: "Locations",
                column: "CustomerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                schema: "Tempus",
                table: "Contacts",
                columns: new[] { "CustomerId", "Id" });

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

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Customers_CustomerId",
                schema: "Tempus",
                table: "Locations",
                column: "CustomerId",
                principalSchema: "Tempus",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Customers_CustomerId",
                schema: "Tempus",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                schema: "Tempus",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Contacts",
                schema: "Tempus",
                table: "Contacts");

            migrationBuilder.DeleteData(
                schema: "Tempus",
                table: "Contacts",
                keyColumns: new[] { "CustomerId", "Id" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                schema: "Tempus",
                table: "Locations",
                keyColumn: "CustomerId",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "Tempus",
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Tempus",
                table: "Locations");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                schema: "Tempus",
                table: "Locations",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                schema: "Tempus",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                schema: "Tempus",
                table: "Contacts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "MiddleName",
                schema: "Tempus",
                table: "Contacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "Tempus",
                table: "Contacts",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                schema: "Tempus",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Contacts",
                schema: "Tempus",
                table: "Contacts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LocationId",
                schema: "Tempus",
                table: "Customers",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CustomerId",
                schema: "Tempus",
                table: "Contacts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Locations_LocationId",
                schema: "Tempus",
                table: "Customers",
                column: "LocationId",
                principalSchema: "Tempus",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
