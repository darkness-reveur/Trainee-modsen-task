using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MeetupPlatformApi.Migrations
{
    public partial class AddFluentApiForRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Meetups",
                table: "Meetups");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "RefreshTokens",
                newName: "refreshTokens");

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
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Expires",
                table: "refreshTokens",
                newName: "expires");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "refreshTokens",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "refreshTokens",
                newName: "user_id");

            migrationBuilder.RenameIndex(
                name: "IX_RefreshTokens_UserId",
                table: "refreshTokens",
                newName: "IX_refreshTokens_user_id");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "meetups",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "meetups",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "meetups",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "meetups",
                newName: "start_time");

            migrationBuilder.RenameColumn(
                name: "EndTime",
                table: "meetups",
                newName: "end_time");

            migrationBuilder.AddPrimaryKey(
                name: "pk_users",
                table: "users",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_refreshTokens",
                table: "refreshTokens",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_meetups",
                table: "meetups",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ux_users_username",
                table: "users",
                column: "username",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_users_refreshTokens",
                table: "refreshTokens",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_users_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_users",
                table: "users");

            migrationBuilder.DropIndex(
                name: "ux_users_username",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "pk_refreshTokens",
                table: "refreshTokens");

            migrationBuilder.DropPrimaryKey(
                name: "pk_meetups",
                table: "meetups");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "refreshTokens",
                newName: "RefreshTokens");

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
                name: "id",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "expires",
                table: "RefreshTokens",
                newName: "Expires");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "RefreshTokens",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "user_id",
                table: "RefreshTokens",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_refreshTokens_user_id",
                table: "RefreshTokens",
                newName: "IX_RefreshTokens_UserId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Meetups",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Meetups",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Meetups",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "start_time",
                table: "Meetups",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "end_time",
                table: "Meetups",
                newName: "EndTime");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RefreshTokens",
                table: "RefreshTokens",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetups",
                table: "Meetups",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshTokens_Users_UserId",
                table: "RefreshTokens",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
