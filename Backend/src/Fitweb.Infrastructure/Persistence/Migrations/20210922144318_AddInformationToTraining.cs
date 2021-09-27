using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class AddInformationToTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "38e1716e-3a7e-4677-83e5-89a4ed1a69f1");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "010a401f-010f-4f81-9c80-ee19a48bae39");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "464b7b2b-e6fc-429e-ad8c-d6b6fcbea1dd", "AQAAAAEAACcQAAAAEN/6tM+J7ZkMZGuGnVq6/HiNWDgBw0hushjgWnz/4X/Mmt+x9t+sVcErmiHaXxKm1A==", "e8551acd-fac2-47b9-a224-b2c305e98aa1" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
