using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class RenameBeginningAndEndToStartDateAndEndDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "End",
                table: "DietInformations",
                newName: "StartDate");

            migrationBuilder.RenameColumn(
                name: "Beginning",
                table: "DietInformations",
                newName: "EndDate");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "52f6d791-65b5-4bcd-8072-0dc713d00890");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "7b301126-d512-4f71-bbae-a919da4c745c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1fa02ad2-785f-461b-9ae9-e248f5cc1101", "AQAAAAEAACcQAAAAEHva+Qriuzvf5Demk5Pq06lElZxFac3PKQPdXfBrPYM+nmR0Wn2kExZvFhoMJnDHGA==", "93f8e53f-4cdd-4616-a117-1972cfa660b9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StartDate",
                table: "DietInformations",
                newName: "End");

            migrationBuilder.RenameColumn(
                name: "EndDate",
                table: "DietInformations",
                newName: "Beginning");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "e93c494f-5313-44ee-8670-36e7b1127d3c");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "94b76870-6eac-4d95-92ed-81efc8d973f9");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "955b51fd-4622-4a0e-a91d-99f553853636", "AQAAAAEAACcQAAAAEMgrKTIHGX3PV0RnR9UL0/qqJd+2f+vAs3eVnnChnSxq7JkpR3hxMpUXfI1qZqojuw==", "5743b779-3d2c-4fbd-a6a2-9f550b5357df" });
        }
    }
}
