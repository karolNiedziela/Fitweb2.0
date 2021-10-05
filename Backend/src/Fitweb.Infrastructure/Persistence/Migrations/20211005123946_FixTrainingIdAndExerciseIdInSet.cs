using Microsoft.EntityFrameworkCore.Migrations;

namespace Fitweb.Infrastructure.Persistence.Migrations
{
    public partial class FixTrainingIdAndExerciseIdInSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_TrainingExercises_TrainingId_ExerciseId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_TrainingId_ExerciseId",
                table: "Sets");

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

            migrationBuilder.CreateIndex(
                name: "IX_Sets_ExerciseId_TrainingId",
                table: "Sets",
                columns: new[] { "ExerciseId", "TrainingId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_TrainingExercises_ExerciseId_TrainingId",
                table: "Sets",
                columns: new[] { "ExerciseId", "TrainingId" },
                principalTable: "TrainingExercises",
                principalColumns: new[] { "ExerciseId", "TrainingId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_TrainingExercises_ExerciseId_TrainingId",
                table: "Sets");

            migrationBuilder.DropIndex(
                name: "IX_Sets_ExerciseId_TrainingId",
                table: "Sets");

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
                name: "IX_Sets_TrainingId_ExerciseId",
                table: "Sets",
                columns: new[] { "TrainingId", "ExerciseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_TrainingExercises_TrainingId_ExerciseId",
                table: "Sets",
                columns: new[] { "TrainingId", "ExerciseId" },
                principalTable: "TrainingExercises",
                principalColumns: new[] { "ExerciseId", "TrainingId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
