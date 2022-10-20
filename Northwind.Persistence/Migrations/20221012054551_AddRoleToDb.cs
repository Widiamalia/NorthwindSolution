using Microsoft.EntityFrameworkCore.Migrations;

namespace Northwind.Persistence.Migrations
{
    public partial class AddRoleToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "695446ef-dd59-4ee2-aa2d-bcdd0c96f457", "995902cb-f56f-4a81-a3e7-c438f790c5e8", "Manager", "MANAGER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c4b313f0-cf99-4456-a63d-30f5bf927b06", "d4fb8e78-41d5-4a23-8c4a-ad27c494ec49", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "695446ef-dd59-4ee2-aa2d-bcdd0c96f457");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4b313f0-cf99-4456-a63d-30f5bf927b06");
        }
    }
}
