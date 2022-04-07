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
                name: "Discriminator",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "meetup_id",
                table: "users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "organizer_id",
                table: "meetups",
                type: "uuid",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_users_meetup_id",
                table: "users",
                column: "meetup_id");

            migrationBuilder.CreateIndex(
                name: "IX_meetups_organizer_id",
                table: "meetups",
                column: "organizer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_meetups_organizer_id",
                table: "meetups",
                column: "organizer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "fk_meetups_users_meetup_id",
                table: "users",
                column: "meetup_id",
                principalTable: "meetups",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_meetups_organizer_id",
                table: "meetups");

            migrationBuilder.DropForeignKey(
                name: "fk_meetups_users_meetup_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_meetup_id",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_meetups_organizer_id",
                table: "meetups");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "users");

            migrationBuilder.DropColumn(
                name: "meetup_id",
                table: "users");

            migrationBuilder.DropColumn(
                name: "organizer_id",
                table: "meetups");
        }
    }
}
