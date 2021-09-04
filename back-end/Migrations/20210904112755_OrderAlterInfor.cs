using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class OrderAlterInfor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "79c201e6-4f95-403f-8826-4cf1dd717581");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "ef0f6546-1b6e-40c1-bd6a-13f1c97b70dd");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "f601eebd-1711-4669-a3f1-3e82753201b4", "9530c411-b393-45c6-a7dd-63e2a231d9ee", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "71bce7fd-d317-4538-ad90-57830a084c5f", "7de87bf9-f910-4016-a8fd-b269f9bc5a2c", "Customer", "CUSTOMER" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "71bce7fd-d317-4538-ad90-57830a084c5f");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "f601eebd-1711-4669-a3f1-3e82753201b4");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "OrderHeaders");

            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "OrderHeaders");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "79c201e6-4f95-403f-8826-4cf1dd717581", "82f6a3ae-fc5e-4750-bf18-bfb265f857c8", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "ef0f6546-1b6e-40c1-bd6a-13f1c97b70dd", "5deccfc7-94d3-47b8-acbc-41311492eb52", "Customer", "CUSTOMER" });
        }
    }
}
