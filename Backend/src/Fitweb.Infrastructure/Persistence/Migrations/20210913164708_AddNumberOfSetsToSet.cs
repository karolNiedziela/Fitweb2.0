using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class AddNumberOfSetsToSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfSets",
                table: "Sets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "5c7623ec-33c1-401d-97bd-86fe5db0135a");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "1bab57cd-17af-4b52-9e6b-07ede309cb05");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "762098ae-fd30-4999-8a02-95e6e44fcf60", "AQAAAAEAACcQAAAAEF6KRXGbVSzm74+ROfigp6qZnFQaFfOPrNlujagbenPSeQ6hEUdAcYKpWlzvnHXfUg==", "f792d64a-a420-4174-9876-251722d645aa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfSets",
                table: "Sets");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "46c0d141-bfcb-4f5e-9eec-16c63f37bf27");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "476ba0b9-8dfb-430a-a5a9-7031466ecd91");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f37b4944-8b2c-4014-8181-bf32daa920b4", "AQAAAAEAACcQAAAAECqdrLSoZ8lTtAGUYC2GGjKSVg+Cz0qD6OKRjhrabM8uuNYWrIHoNLnJHD2hR6S3FA==", "cfca86b9-d609-42cc-9c69-5e217901fce4" });
        }
    }
}
