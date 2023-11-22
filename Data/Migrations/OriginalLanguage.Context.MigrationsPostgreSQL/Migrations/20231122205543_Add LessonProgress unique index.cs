using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddLessonProgressuniqueindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_lesson_progresses_LessonId",
                table: "lesson_progresses");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_progresses_LessonId_UserId",
                table: "lesson_progresses",
                columns: new[] { "LessonId", "UserId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_lesson_progresses_LessonId_UserId",
                table: "lesson_progresses");

            migrationBuilder.CreateIndex(
                name: "IX_lesson_progresses_LessonId",
                table: "lesson_progresses",
                column: "LessonId");
        }
    }
}
