using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateLanguageandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FullName",
                table: "users");

            migrationBuilder.DropColumn(
                name: "IsConlang",
                table: "languages");

            migrationBuilder.RenameColumn(
                name: "DateTimeAdded",
                table: "languages",
                newName: "DateTimeUpdated");

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegistrationDate",
                table: "users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<string>(
                name: "NativeName",
                table: "languages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "languages",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AboutNativeSpeakers",
                table: "languages",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConlangDataId",
                table: "languages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateTimeCreated",
                table: "languages",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "FlagUrl",
                table: "languages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Links",
                table: "languages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "articles",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "conlang_data",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Origin = table.Column<int>(type: "integer", nullable: false),
                    Articulation = table.Column<int>(type: "integer", nullable: false),
                    SubjectiveComplexity = table.Column<int>(type: "integer", nullable: false),
                    DevelopmentStatus = table.Column<int>(type: "integer", nullable: false),
                    SettingOrigin = table.Column<int>(type: "integer", nullable: false),
                    NotHumanoidSpeakers = table.Column<bool>(type: "boolean", nullable: false),
                    FurrySpeakers = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conlang_data", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_languages_ConlangDataId",
                table: "languages",
                column: "ConlangDataId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_articles_LanguageId",
                table: "articles",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_articles_languages_LanguageId",
                table: "articles",
                column: "LanguageId",
                principalTable: "languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_languages_conlang_data_ConlangDataId",
                table: "languages",
                column: "ConlangDataId",
                principalTable: "conlang_data",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_articles_languages_LanguageId",
                table: "articles");

            migrationBuilder.DropForeignKey(
                name: "FK_languages_conlang_data_ConlangDataId",
                table: "languages");

            migrationBuilder.DropTable(
                name: "conlang_data");

            migrationBuilder.DropIndex(
                name: "IX_languages_ConlangDataId",
                table: "languages");

            migrationBuilder.DropIndex(
                name: "IX_articles_LanguageId",
                table: "articles");

            migrationBuilder.DropColumn(
                name: "About",
                table: "users");

            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "users");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "users");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "users");

            migrationBuilder.DropColumn(
                name: "About",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "AboutNativeSpeakers",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "ConlangDataId",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "DateTimeCreated",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "FlagUrl",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "Links",
                table: "languages");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "articles");

            migrationBuilder.RenameColumn(
                name: "DateTimeUpdated",
                table: "languages",
                newName: "DateTimeAdded");

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NativeName",
                table: "languages",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsConlang",
                table: "languages",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
