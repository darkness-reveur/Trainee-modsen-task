using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class MeetupPKFromIntToGuid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_meetups",
                table: "meetups");

            migrationBuilder.DropColumn(
                name: "id",
                table: "meetups");

            migrationBuilder.AddColumn<Guid>(
                name: "id", 
                table: "meetups",
                nullable: false,
                defaultValue: Guid.Empty);

            migrationBuilder.AddPrimaryKey(
                name: "pk_meetups",
                table: "meetups",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Meetups",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
