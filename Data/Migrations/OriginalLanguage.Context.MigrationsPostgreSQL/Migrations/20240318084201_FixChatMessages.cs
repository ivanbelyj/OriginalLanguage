using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OriginalLanguage.Context.MigrationsPostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class FixChatMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvatarUrl",
                table: "chat_messages");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "chat_messages");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "chat_messages",
                type: "uuid",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "chat_messages");

            migrationBuilder.AddColumn<string>(
                name: "AvatarUrl",
                table: "chat_messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "chat_messages",
                type: "text",
                nullable: true);
        }
    }
}
