using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class AddDateToTraining : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Trainings",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "dc9f93ff-f32c-464c-a2a4-67d869842f5e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "2de686e9-2853-43b3-95c7-e27541d02b0c");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0072fd91-09e9-44df-853b-8d2c6175b64d", "AQAAAAEAACcQAAAAEAltVqoDyjyH5wHXs7zkXZ0lCqAMLpSPTw2lB0NmBw0EN5l+IMLeCcw7LkjiK+UF8A==", "e3abf403-f82e-4f2f-a38a-9e3f6fd9981f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Trainings");

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
    }
}
