using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class EnumToStringConversion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Group",
                table: "FoodProducts");

            migrationBuilder.AlterColumn<string>(
                name: "Day",
                table: "Trainings",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "FoodGroup",
                table: "FoodProducts",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "PartOfBody",
                table: "Exercises",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "846b9436-11bf-4686-8d4e-88e5f08a0fa9");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "4aedd078-6afa-46a2-9914-5dce9a41c315");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "692387db-9e21-42a6-b821-242d3c3b1851", "AQAAAAEAACcQAAAAEJvpsooiKR9eNLhYEWjUVR4Mgvwb8GvGrit3Vb53LrVgNeO0xOI0TPT/IsR3QhvX8Q==", "d55ed7df-69de-436b-8b69-eb3eb1c010f7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FoodGroup",
                table: "FoodProducts");

            migrationBuilder.AlterColumn<int>(
                name: "Day",
                table: "Trainings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Group",
                table: "FoodProducts",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PartOfBody",
                table: "Exercises",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "e8e89346-7f0b-4010-8bf0-3887bc1d0bad");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "6fc6dbf4-4668-46dc-a9c1-5bf1ed80655b");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7786c8f6-95e4-44d4-a0b9-ac3d8ed01adf", "AQAAAAEAACcQAAAAEOit4l9prQY+H/gjhj0sWnjDFWNEeEOvC9A4qesM9uSiyz6pvAT5WHcFVmpmiF1WPQ==", "3c66b23e-35dd-4acc-92b5-a6149d2b5d08" });
        }
    }
}
