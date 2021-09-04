using Microsoft.EntityFrameworkCore.Migrations;

namespace back_end.Migrations
{
    public partial class PrdDesc : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5dc57a5e-5554-4a24-8ca0-256d9f9957c1");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5e559099-44e9-43b9-9e02-ccceb86d843d");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "Description",
                value: "Đơn giản là đẹp");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "Description",
                value: "Cực kì cá tính");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b70c5add-b539-41ca-b540-1ee7307e7c2e", "97e0fec2-2ca8-4118-b4b5-9624610cfbc0", "Admin", "ADMIN" },
                    { "5513d1ed-37da-49a3-8fdd-d2f06f6d270b", "553d4b68-fc07-4b02-936b-a27840d85183", "Customer", "CUSTOMER" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "5513d1ed-37da-49a3-8fdd-d2f06f6d270b");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "b70c5add-b539-41ca-b540-1ee7307e7c2e");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5e559099-44e9-43b9-9e02-ccceb86d843d", "ed615ca7-37ce-4895-bc85-76417ad582d7", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "5dc57a5e-5554-4a24-8ca0-256d9f9957c1", "f06385ea-81b5-46b9-9701-920acc689f17", "Customer", "CUSTOMER" });
        }
    }
}
