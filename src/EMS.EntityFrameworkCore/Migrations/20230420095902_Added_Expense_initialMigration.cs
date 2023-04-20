using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpenseinitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AbpUsers_paidBy",
                table: "AppExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AppGroups_groupId",
                table: "AppExpenses");

            migrationBuilder.RenameColumn(
                name: "paidBy",
                table: "AppExpenses",
                newName: "PaidBy");

            migrationBuilder.RenameColumn(
                name: "groupId",
                table: "AppExpenses",
                newName: "GroupId");

            migrationBuilder.RenameIndex(
                name: "IX_AppExpenses_paidBy",
                table: "AppExpenses",
                newName: "IX_AppExpenses_PaidBy");

            migrationBuilder.RenameIndex(
                name: "IX_AppExpenses_groupId",
                table: "AppExpenses",
                newName: "IX_AppExpenses_GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AbpUsers_PaidBy",
                table: "AppExpenses",
                column: "PaidBy",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AppGroups_GroupId",
                table: "AppExpenses",
                column: "GroupId",
                principalTable: "AppGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AbpUsers_PaidBy",
                table: "AppExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AppGroups_GroupId",
                table: "AppExpenses");

            migrationBuilder.RenameColumn(
                name: "PaidBy",
                table: "AppExpenses",
                newName: "paidBy");

            migrationBuilder.RenameColumn(
                name: "GroupId",
                table: "AppExpenses",
                newName: "groupId");

            migrationBuilder.RenameIndex(
                name: "IX_AppExpenses_PaidBy",
                table: "AppExpenses",
                newName: "IX_AppExpenses_paidBy");

            migrationBuilder.RenameIndex(
                name: "IX_AppExpenses_GroupId",
                table: "AppExpenses",
                newName: "IX_AppExpenses_groupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AbpUsers_paidBy",
                table: "AppExpenses",
                column: "paidBy",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppExpenses_AppGroups_groupId",
                table: "AppExpenses",
                column: "groupId",
                principalTable: "AppGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
