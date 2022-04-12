using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddKeyword : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IX_refresh_tokens_user_id",
                table: "refresh_tokens",
                newName: "ix_refresh_tokens_user_id");

            migrationBuilder.CreateTable(
                name: "keywords",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    keyword_name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_keywords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "meetups_keywords",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, defaultValueSql: "gen_random_uuid()"),
                    meetup_id = table.Column<Guid>(type: "uuid", nullable: false),
                    keyword_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meetups_keywords", x => x.id);
                    table.ForeignKey(
                        name: "fk_keywords_meetups_keywords_keyword_id",
                        column: x => x.keyword_id,
                        principalTable: "keywords",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meetups_meetups_keywords_meetup_id",
                        column: x => x.meetup_id,
                        principalTable: "meetups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_meetups_keywords_keyword_id",
                table: "meetups_keywords",
                column: "keyword_id");

            migrationBuilder.CreateIndex(
                name: "ix_meetups_keywords_meetup_id",
                table: "meetups_keywords",
                column: "meetup_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meetups_keywords");

            migrationBuilder.DropTable(
                name: "keywords");

            migrationBuilder.RenameIndex(
                name: "ix_refresh_tokens_user_id",
                table: "refresh_tokens",
                newName: "IX_refresh_tokens_user_id");
        }
    }
}
