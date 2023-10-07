using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class RenameTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_languages_LanguageId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonProgress_Lessons_LessonId",
                table: "LessonProgress");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_articles_TheoryArticleId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonSample_Lessons_LessonId",
                table: "LessonSample");

            migrationBuilder.DropForeignKey(
                name: "FK_LessonSample_Sentences_MainSentenceVariantId",
                table: "LessonSample");

            migrationBuilder.DropForeignKey(
                name: "FK_Sentences_LessonSample_LessonSampleId",
                table: "Sentences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sentences",
                table: "Sentences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Courses",
                table: "Courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StaticPages",
                table: "StaticPages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonSample",
                table: "LessonSample");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LessonProgress",
                table: "LessonProgress");

            migrationBuilder.RenameTable(
                name: "Sentences",
                newName: "sentences");

            migrationBuilder.RenameTable(
                name: "Lessons",
                newName: "lessons");

            migrationBuilder.RenameTable(
                name: "Courses",
                newName: "courses");

            migrationBuilder.RenameTable(
                name: "StaticPages",
                newName: "static_pages");

            migrationBuilder.RenameTable(
                name: "LessonSample",
                newName: "lesson_samples");

            migrationBuilder.RenameTable(
                name: "LessonProgress",
                newName: "lesson_progresses");

            migrationBuilder.RenameIndex(
                name: "IX_Sentences_LessonSampleId",
                table: "sentences",
                newName: "IX_sentences_LessonSampleId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_TheoryArticleId",
                table: "lessons",
                newName: "IX_lessons_TheoryArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_Lessons_CourseId",
                table: "lessons",
                newName: "IX_lessons_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_Courses_LanguageId",
                table: "courses",
                newName: "IX_courses_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonSample_MainSentenceVariantId",
                table: "lesson_samples",
                newName: "IX_lesson_samples_MainSentenceVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonSample_LessonId",
                table: "lesson_samples",
                newName: "IX_lesson_samples_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_LessonProgress_LessonId",
                table: "lesson_progresses",
                newName: "IX_lesson_progresses_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_sentences",
                table: "sentences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lessons",
                table: "lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_courses",
                table: "courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_static_pages",
                table: "static_pages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_samples",
                table: "lesson_samples",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_lesson_progresses",
                table: "lesson_progresses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_languages_LanguageId",
                table: "courses",
                column: "LanguageId",
                principalTable: "languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_progresses_lessons_LessonId",
                table: "lesson_progresses",
                column: "LessonId",
                principalTable: "lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_samples_lessons_LessonId",
                table: "lesson_samples",
                column: "LessonId",
                principalTable: "lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples",
                column: "MainSentenceVariantId",
                principalTable: "sentences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lessons_articles_TheoryArticleId",
                table: "lessons",
                column: "TheoryArticleId",
                principalTable: "articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_lessons_courses_CourseId",
                table: "lessons",
                column: "CourseId",
                principalTable: "courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_sentences_lesson_samples_LessonSampleId",
                table: "sentences",
                column: "LessonSampleId",
                principalTable: "lesson_samples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_courses_languages_LanguageId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_progresses_lessons_LessonId",
                table: "lesson_progresses");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_samples_lessons_LessonId",
                table: "lesson_samples");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_samples_sentences_MainSentenceVariantId",
                table: "lesson_samples");

            migrationBuilder.DropForeignKey(
                name: "FK_lessons_articles_TheoryArticleId",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_lessons_courses_CourseId",
                table: "lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_sentences_lesson_samples_LessonSampleId",
                table: "sentences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_sentences",
                table: "sentences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lessons",
                table: "lessons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_courses",
                table: "courses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_static_pages",
                table: "static_pages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_samples",
                table: "lesson_samples");

            migrationBuilder.DropPrimaryKey(
                name: "PK_lesson_progresses",
                table: "lesson_progresses");

            migrationBuilder.RenameTable(
                name: "sentences",
                newName: "Sentences");

            migrationBuilder.RenameTable(
                name: "lessons",
                newName: "Lessons");

            migrationBuilder.RenameTable(
                name: "courses",
                newName: "Courses");

            migrationBuilder.RenameTable(
                name: "static_pages",
                newName: "StaticPages");

            migrationBuilder.RenameTable(
                name: "lesson_samples",
                newName: "LessonSample");

            migrationBuilder.RenameTable(
                name: "lesson_progresses",
                newName: "LessonProgress");

            migrationBuilder.RenameIndex(
                name: "IX_sentences_LessonSampleId",
                table: "Sentences",
                newName: "IX_Sentences_LessonSampleId");

            migrationBuilder.RenameIndex(
                name: "IX_lessons_TheoryArticleId",
                table: "Lessons",
                newName: "IX_Lessons_TheoryArticleId");

            migrationBuilder.RenameIndex(
                name: "IX_lessons_CourseId",
                table: "Lessons",
                newName: "IX_Lessons_CourseId");

            migrationBuilder.RenameIndex(
                name: "IX_courses_LanguageId",
                table: "Courses",
                newName: "IX_Courses_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_lesson_samples_MainSentenceVariantId",
                table: "LessonSample",
                newName: "IX_LessonSample_MainSentenceVariantId");

            migrationBuilder.RenameIndex(
                name: "IX_lesson_samples_LessonId",
                table: "LessonSample",
                newName: "IX_LessonSample_LessonId");

            migrationBuilder.RenameIndex(
                name: "IX_lesson_progresses_LessonId",
                table: "LessonProgress",
                newName: "IX_LessonProgress_LessonId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sentences",
                table: "Sentences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Lessons",
                table: "Lessons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Courses",
                table: "Courses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StaticPages",
                table: "StaticPages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonSample",
                table: "LessonSample",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LessonProgress",
                table: "LessonProgress",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_languages_LanguageId",
                table: "Courses",
                column: "LanguageId",
                principalTable: "languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonProgress_Lessons_LessonId",
                table: "LessonProgress",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Courses_CourseId",
                table: "Lessons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_articles_TheoryArticleId",
                table: "Lessons",
                column: "TheoryArticleId",
                principalTable: "articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSample_Lessons_LessonId",
                table: "LessonSample",
                column: "LessonId",
                principalTable: "Lessons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LessonSample_Sentences_MainSentenceVariantId",
                table: "LessonSample",
                column: "MainSentenceVariantId",
                principalTable: "Sentences",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sentences_LessonSample_LessonSampleId",
                table: "Sentences",
                column: "LessonSampleId",
                principalTable: "LessonSample",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
