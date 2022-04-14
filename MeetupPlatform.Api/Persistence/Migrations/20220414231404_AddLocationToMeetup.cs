using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatform.Api.Migrations
{
    public partial class AddLocationToMeetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "location",
                table: "meetups",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "location",
                table: "meetups");
        }
    }
}
