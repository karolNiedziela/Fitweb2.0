using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class SetFoodGroupToNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Group",
                table: "FoodProducts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "c93ea7f7-a58c-47c7-a8ee-9a5e2bdc5b15");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "7a2aba99-d4e9-4a3b-a6b3-d68c2f3b0e9d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bb8d88c2-9c97-49ed-98da-ce1340a8f84c", "AQAAAAEAACcQAAAAEG5uQ1YQlUEymYbxTozV+9e2OxNvhFU7T5HDRIYTlBDQfz1vQQj2wXhlFzdcAX2wMQ==", "ccfb4cd3-47c8-4773-8fcd-d59b26bc8679" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Group",
                table: "FoodProducts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "327e3158-488d-48a8-9f2e-2c6e3c0404d5");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "a3050c40-83cc-4b5c-9811-b9504b9474e7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28812734-1f71-4021-9443-cfab32efd2b7", "AQAAAAEAACcQAAAAEKDi6DF9sUB3kaajiyyOCRhrkqRYeWp2o0fEuIf7cy887nbhnLmkV9wnWXmG1ULRMA==", "51d84866-80c7-4984-89b4-da3e1b1a4f21" });
        }
    }
}
