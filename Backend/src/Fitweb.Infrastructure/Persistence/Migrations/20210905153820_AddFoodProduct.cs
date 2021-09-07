using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class AddFoodProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FoodProducts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Calories = table.Column<double>(type: "double", nullable: true),
                    Protein = table.Column<double>(type: "double", nullable: true),
                    Carbohydrate = table.Column<double>(type: "double", nullable: true),
                    Sugar = table.Column<double>(type: "double", nullable: true),
                    Fat = table.Column<double>(type: "double", nullable: true),
                    SaturatedFat = table.Column<double>(type: "double", nullable: true),
                    Fiber = table.Column<double>(type: "double", nullable: true),
                    Salt = table.Column<double>(type: "double", nullable: true),
                    Group = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodProducts", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FoodProducts");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "33a50528-af11-4067-9f69-618e11ab356e");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "804a813a-84e1-4a1e-a103-b813b50b5fe7");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9f9346e0-6e23-406e-a7f2-a6aefa6ea577", "AQAAAAEAACcQAAAAEHGUahzLLgx4HK/8AXtbjhTfA+ZFCxMHHcTRKVzIUsvyF2Y56nIU7fVzHHRi04lOuA==", "a7268cc6-9468-416d-ae15-09857522c64c" });
        }
    }
}
