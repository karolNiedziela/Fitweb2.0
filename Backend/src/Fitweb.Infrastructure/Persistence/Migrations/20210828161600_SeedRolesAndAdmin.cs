using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class SeedRolesAndAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "a792b6cb-8230-4a37-9353-1a05d642ffe2", "804a813a-84e1-4a1e-a103-b813b50b5fe7", "Administrator", "ADMINISTRATOR" });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9dd36f65-1fc5-4383-b7af-626d5bd60728", "33a50528-af11-4067-9f69-618e11ab356e", "Athlete", "ATHLETE" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "ff48a62e-0e06-47a2-aacb-c88af07993ed", 0, "9f9346e0-6e23-406e-a7f2-a6aefa6ea577", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMINISTRATOR", "AQAAAAEAACcQAAAAEHGUahzLLgx4HK/8AXtbjhTfA+ZFCxMHHcTRKVzIUsvyF2Y56nIU7fVzHHRi04lOuA==", null, false, "a7268cc6-9468-416d-ae15-09857522c64c", false, "administrator" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "a792b6cb-8230-4a37-9353-1a05d642ffe2", "ff48a62e-0e06-47a2-aacb-c88af07993ed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "a792b6cb-8230-4a37-9353-1a05d642ffe2", "ff48a62e-0e06-47a2-aacb-c88af07993ed" });

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed");
        }
    }
}
