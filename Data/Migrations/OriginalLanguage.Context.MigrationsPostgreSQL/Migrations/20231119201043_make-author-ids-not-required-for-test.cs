using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class makeauthoridsnotrequiredfortest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_users_AuthorId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_languages_users_AuthorId",
                table: "languages");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_progresses_users_UserId",
                table: "lesson_progresses");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_courses_users_AuthorId",
                table: "courses",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_languages_users_AuthorId",
                table: "languages",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_progresses_users_UserId",
                table: "lesson_progresses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles");

            migrationBuilder.DropForeignKey(
                name: "FK_courses_users_AuthorId",
                table: "courses");

            migrationBuilder.DropForeignKey(
                name: "FK_languages_users_AuthorId",
                table: "languages");

            migrationBuilder.DropForeignKey(
                name: "FK_lesson_progresses_users_UserId",
                table: "lesson_progresses");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_users_AuthorId",
                table: "courses",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_languages_users_AuthorId",
                table: "languages",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_lesson_progresses_users_UserId",
                table: "lesson_progresses",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
