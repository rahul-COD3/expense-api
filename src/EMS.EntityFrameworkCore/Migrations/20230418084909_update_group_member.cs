using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class update_group_member : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppGroupMembers_groupId",
                table: "AppGroupMembers",
                column: "groupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppGroupMembers_AppGroups_groupId",
                table: "AppGroupMembers",
                column: "groupId",
                principalTable: "AppGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppGroupMembers_AppGroups_groupId",
                table: "AppGroupMembers");

            migrationBuilder.DropIndex(
                name: "IX_AppGroupMembers_groupId",
                table: "AppGroupMembers");
        }
    }
}
