using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class OrderHeaderDeliveryAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9e25392b-41cc-4902-be99-f9495882b9da");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f219fc73-e757-4ca6-9eeb-d897e8769e26");

            migrationBuilder.AddColumn<string>(
                name: "DeliveryAddress",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e559099-44e9-43b9-9e02-ccceb86d843d", "ed615ca7-37ce-4895-bc85-76417ad582d7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dc57a5e-5554-4a24-8ca0-256d9f9957c1", "f06385ea-81b5-46b9-9701-920acc689f17", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5dc57a5e-5554-4a24-8ca0-256d9f9957c1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5e559099-44e9-43b9-9e02-ccceb86d843d");

            migrationBuilder.DropColumn(
                name: "DeliveryAddress",
                table: "OrderHeaders");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f219fc73-e757-4ca6-9eeb-d897e8769e26", "2d6a0628-366e-4aa5-b82d-c73565784094", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9e25392b-41cc-4902-be99-f9495882b9da", "743dd714-45af-4808-b2d2-ac5c19ec6101", "Customer", "CUSTOMER" });
        }
    }
}
