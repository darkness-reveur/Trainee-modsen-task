using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddUsersByRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "role",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "organizer_id",
                table: "meetups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "meetups_plain_users",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    meetup_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meetups_plain_users", x => new { x.user_id, x.meetup_id });
                    table.ForeignKey(
                        name: "fk_meetups_plain_users_meetups_meetup_id",
                        column: x => x.meetup_id,
                        principalTable: "meetups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meetups_plain_users_plain_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_meetups_organizer_id",
                table: "meetups",
                column: "organizer_id");

            migrationBuilder.CreateIndex(
                name: "IX_meetups_plain_users_meetup_id",
                table: "meetups_plain_users",
                column: "meetup_id");

            migrationBuilder.CreateIndex(
                name: "ix_meetups_plain_users_user_id",
                table: "meetups_plain_users",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_meetups_organizer_id",
                table: "meetups",
                column: "organizer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_meetups_organizer_id",
                table: "meetups");

            migrationBuilder.DropTable(
                name: "meetups_plain_users");

            migrationBuilder.DropIndex(
                name: "ix_meetups_organizer_id",
                table: "meetups");

            migrationBuilder.DropColumn(
                name: "role",
                table: "users");

            migrationBuilder.DropColumn(
                name: "organizer_id",
                table: "meetups");
        }
    }
}
