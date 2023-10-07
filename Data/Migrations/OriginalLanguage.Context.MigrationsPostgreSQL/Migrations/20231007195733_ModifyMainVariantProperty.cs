using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMainVariantProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples");

            migrationBuilder.AlterColumn<int>(
                name: "MainSentenceVariantId",
                table: "lesson_samples",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples",
                column: "MainSentenceVariantId",
                principalTable: "sentences",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples");

            migrationBuilder.AlterColumn<int>(
                name: "MainSentenceVariantId",
                table: "lesson_samples",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples",
                column: "MainSentenceVariantId",
                principalTable: "sentences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
