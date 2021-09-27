using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class AddUserIdToFoodProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "FoodProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "2871c614-5cf0-47cb-adad-e6417e018cf8");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "8988b910-4d71-402e-a765-4a76ba818fd6");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e65c47b8-9e2b-400d-8572-236f8bf4cb35", "AQAAAAEAACcQAAAAEGkXdoUhaTM5RSKD6iuQuc8heeHryAPlyqjdmQ16RISUD6nDRvXTnmCDHvCiTdtlEQ==", "d8e3ff1c-0e05-4904-8f26-c79c241afe17" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FoodProducts");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "5b1e90e0-e980-4b46-ba9e-62df14813913");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "a9ff8c14-f5c5-4cce-b27f-a3644634674d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "078b7d59-dcc5-40a4-8a5f-313521533f63", "AQAAAAEAACcQAAAAEEJYkrnn9qAKGnb+vYxKjewcZGMypQpJoZ9l6Wdfo++2TnUTZZA0gIQHFCOGmhxKQA==", "589daf4b-d5f9-48bb-8916-7a3574264d59" });
        }
    }
}
