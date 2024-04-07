using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class MoveSomeSentenceColumnsToLessonSample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples");

            migrationBuilder.DropIndex(
                name: "IX_lesson_samples_MainSentenceVariantId",
                table: "lesson_samples");

            migrationBuilder.DropColumn(
                name: "Glosses",
                table: "sentences");

            migrationBuilder.DropColumn(
                name: "LiteralTranslation",
                table: "sentences");

            migrationBuilder.DropColumn(
                name: "Transcription",
                table: "sentences");

            migrationBuilder.DropColumn(
                name: "MainSentenceVariantId",
                table: "lesson_samples");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "lessons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "lessons",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Glosses",
                table: "lesson_samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainText",
                table: "lesson_samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainTranslation",
                table: "lesson_samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TextHints",
                table: "lesson_samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transcription",
                table: "lesson_samples",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TranslationHints",
                table: "lesson_samples",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "lessons");

            migrationBuilder.DropColumn(
                name: "Glosses",
                table: "lesson_samples");

            migrationBuilder.DropColumn(
                name: "MainText",
                table: "lesson_samples");

            migrationBuilder.DropColumn(
                name: "MainTranslation",
                table: "lesson_samples");

            migrationBuilder.DropColumn(
                name: "TextHints",
                table: "lesson_samples");

            migrationBuilder.DropColumn(
                name: "Transcription",
                table: "lesson_samples");

            migrationBuilder.DropColumn(
                name: "TranslationHints",
                table: "lesson_samples");

            migrationBuilder.AddColumn<string>(
                name: "Glosses",
                table: "sentences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LiteralTranslation",
                table: "sentences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transcription",
                table: "sentences",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainSentenceVariantId",
                table: "lesson_samples",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_lesson_samples_MainSentenceVariantId",
                table: "lesson_samples",
                column: "MainSentenceVariantId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples",
                column: "MainSentenceVariantId",
                principalTable: "sentences",
                principalColumn: "Id");
        }
    }
}
