using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddMeetupOrganizer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "organizer_id",
                table: "meetups",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_meetups_organizer_id",
                table: "meetups",
                column: "organizer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_users_meetups_user_id",
                table: "meetups",
                column: "organizer_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_meetups_user_id",
                table: "meetups");

            migrationBuilder.DropIndex(
                name: "IX_meetups_organizer_id",
                table: "meetups");

            migrationBuilder.DropColumn(
                name: "organizer_id",
                table: "meetups");
        }
    }
}
