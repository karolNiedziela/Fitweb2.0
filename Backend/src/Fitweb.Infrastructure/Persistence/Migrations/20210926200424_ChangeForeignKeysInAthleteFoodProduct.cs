using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class ChangeForeignKeysInAthleteFoodProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AthleteFoodProducts_Athletes_FoodProductId",
                table: "AthleteFoodProducts");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "9dd36f65-1fc5-4383-b7af-626d5bd60728",
                column: "ConcurrencyStamp",
                value: "790b1318-eb76-42c2-a411-edf812261e16");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "a792b6cb-8230-4a37-9353-1a05d642ffe2",
                column: "ConcurrencyStamp",
                value: "c8b41a5f-3329-4095-ad32-c98389a2634d");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "ff48a62e-0e06-47a2-aacb-c88af07993ed",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65d6d0e9-1f8f-4d34-98f7-890a5d2ab2d8", "AQAAAAEAACcQAAAAEMu5R+bTs43elzo4+Te2Bfydc4Bpurrxp5Tk/c1Bq38YPf8jN7AurIRKhZJeECoW9Q==", "19ae77ac-ca60-4c79-a454-528d49a83b44" });

            migrationBuilder.CreateIndex(
                name: "IX_AthleteFoodProducts_AthleteId_FoodProductId",
                table: "AthleteFoodProducts",
                columns: new[] { "AthleteId", "FoodProductId" });

            migrationBuilder.AddForeignKey(
                name: "FK_AthleteFoodProducts_Athletes_AthleteId",
                table: "AthleteFoodProducts",
                column: "AthleteId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AthleteFoodProducts_Athletes_AthleteId",
                table: "AthleteFoodProducts");

            migrationBuilder.DropIndex(
                name: "IX_AthleteFoodProducts_AthleteId_FoodProductId",
                table: "AthleteFoodProducts");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AthleteFoodProducts_Athletes_FoodProductId",
                table: "AthleteFoodProducts",
                column: "FoodProductId",
                principalTable: "Athletes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
