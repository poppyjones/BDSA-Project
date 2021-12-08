using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace server.Migrations
{
    public partial class MigTest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Posts_UserId",
                table: "PostUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Users_PostId",
                table: "PostUser");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "PostUser",
                newName: "CollaboratingUsersUserId");

            migrationBuilder.RenameColumn(
                name: "PostId",
                table: "PostUser",
                newName: "CollaboratingPostsId");

            migrationBuilder.RenameIndex(
                name: "IX_PostUser_UserId",
                table: "PostUser",
                newName: "IX_PostUser_CollaboratingUsersUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Posts_CollaboratingPostsId",
                table: "PostUser",
                column: "CollaboratingPostsId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Users_CollaboratingUsersUserId",
                table: "PostUser",
                column: "CollaboratingUsersUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Posts_CollaboratingPostsId",
                table: "PostUser");

            migrationBuilder.DropForeignKey(
                name: "FK_PostUser_Users_CollaboratingUsersUserId",
                table: "PostUser");

            migrationBuilder.RenameColumn(
                name: "CollaboratingUsersUserId",
                table: "PostUser",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "CollaboratingPostsId",
                table: "PostUser",
                newName: "PostId");

            migrationBuilder.RenameIndex(
                name: "IX_PostUser_CollaboratingUsersUserId",
                table: "PostUser",
                newName: "IX_PostUser_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Posts_UserId",
                table: "PostUser",
                column: "UserId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostUser_Users_PostId",
                table: "PostUser",
                column: "PostId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
