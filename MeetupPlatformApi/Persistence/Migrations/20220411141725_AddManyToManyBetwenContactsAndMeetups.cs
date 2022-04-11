using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddManyToManyBetwenContactsAndMeetups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "meetups_contacts",
                columns: table => new
                {
                    contact_id = table.Column<Guid>(type: "uuid", nullable: false),
                    meetup_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_meetups_contacts", x => new { x.contact_id, x.meetup_id });
                    table.ForeignKey(
                        name: "fk_meetups_contacts_contacts_contact_id",
                        column: x => x.contact_id,
                        principalTable: "contacts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_meetups_contacts_meetups_meetup_id",
                        column: x => x.meetup_id,
                        principalTable: "meetups",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_meetups_contacts_meetup_id",
                table: "meetups_contacts",
                column: "meetup_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "meetups_contacts");
        }
    }
}
