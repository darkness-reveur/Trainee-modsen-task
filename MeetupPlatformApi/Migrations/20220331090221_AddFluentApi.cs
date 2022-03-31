using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddFluentApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetups",
                table: "Meetups");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Meetups",
                newName: "meetups");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "users",
                newName: "username");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "users",
                newName: "password");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "users",
                newName: "pk_users");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "meetups",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "meetups",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "meetups",
                newName: "start_time");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "meetups",
                newName: "end_time");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "meetups",
                newName: "pk_meetups");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "pk_users");

            migrationBuilder.AddPrimaryKey(
                name: "pk_meetups",
                table: "meetups",
                column: "pk_meetups");

            migrationBuilder.CreateIndex(
                name: "ux_users_username",
                table: "users",
                column: "username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ux_users_username",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_meetups",
                table: "meetups");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "meetups",
                newName: "Meetups");

            migrationBuilder.RenameColumn(
                name: "username",
                table: "Users",
                newName: "Username");

            migrationBuilder.RenameColumn(
                name: "password",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "pk_users",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Meetups",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Meetups",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "start_time",
                table: "Meetups",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "end_time",
                table: "Meetups",
                newName: "EndTime");

            migrationBuilder.RenameColumn(
                name: "pk_meetups",
                table: "Meetups",
                newName: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetups",
                table: "Meetups",
                column: "Id");
        }
    }
}
