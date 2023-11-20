using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDeleteBehaviour : Migration
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

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "articles",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_users_AuthorId",
                table: "articles",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_courses_users_AuthorId",
                table: "courses",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_languages_users_AuthorId",
                table: "languages",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
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

            migrationBuilder.AlterColumn<Guid>(
                name: "AuthorId",
                table: "articles",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

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
        }
    }
}
