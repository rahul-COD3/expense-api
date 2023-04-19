using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class FriendEntitywithRealtion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppFriends_FriendId",
                table: "AppFriends",
                column: "FriendId");

            migrationBuilder.CreateIndex(
                name: "IX_AppFriends_UserId",
                table: "AppFriends",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppFriends_AbpUsers_FriendId",
                table: "AppFriends",
                column: "FriendId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppFriends_AbpUsers_UserId",
                table: "AppFriends",
                column: "UserId",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppFriends_AbpUsers_FriendId",
                table: "AppFriends");

            migrationBuilder.DropForeignKey(
                name: "FK_AppFriends_AbpUsers_UserId",
                table: "AppFriends");

            migrationBuilder.DropIndex(
                name: "IX_AppFriends_FriendId",
                table: "AppFriends");

            migrationBuilder.DropIndex(
                name: "IX_AppFriends_UserId",
                table: "AppFriends");
        }
    }
}
