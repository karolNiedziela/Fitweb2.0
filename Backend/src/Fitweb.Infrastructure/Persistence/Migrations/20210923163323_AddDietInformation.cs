using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class AddDietInformation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DietInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TotalCalories = table.Column<double>(type: "double", nullable: false),
                    TotalProteins = table.Column<double>(type: "double", nullable: false),
                    TotalCarbohydrates = table.Column<double>(type: "double", nullable: false),
                    TotalFats = table.Column<double>(type: "double", nullable: false),
                    Beginning = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    End = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    AthleteId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietInformations_Athletes_AthleteId",
                        column: x => x.AthleteId,
                        principalTable: "Athletes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

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

            migrationBuilder.CreateIndex(
                name: "IX_DietInformations_AthleteId",
                table: "DietInformations",
                column: "AthleteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietInformations");

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
    }
}
