using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpenseUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "paid_by",
                table: "AppExpenses",
                newName: "paidBy");

            migrationBuilder.RenameColumn(
                name: "group_id",
                table: "AppExpenses",
                newName: "groupId");

            migrationBuilder.AlterColumn<string>(
                name: "expense_title",
                table: "AppExpenses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(128)",
                oldMaxLength: 128);

            migrationBuilder.AddColumn<bool>(
                name: "IsSettled",
                table: "AppExpenses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_AppExpenses_groupId",
                table: "AppExpenses",
                column: "groupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppExpenses_paidBy",
                table: "AppExpenses",
                column: "paidBy");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AbpUsers_paidBy",
                table: "AppExpenses");

            migrationBuilder.DropForeignKey(
                name: "FK_AppExpenses_AppGroups_groupId",
                table: "AppExpenses");

            migrationBuilder.DropIndex(
                name: "IX_AppExpenses_groupId",
                table: "AppExpenses");

            migrationBuilder.DropIndex(
                name: "IX_AppExpenses_paidBy",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "IsSettled",
                table: "AppExpenses");

            migrationBuilder.RenameColumn(
                name: "paidBy",
                table: "AppExpenses",
                newName: "paid_by");

            migrationBuilder.RenameColumn(
                name: "groupId",
                table: "AppExpenses",
                newName: "group_id");

            migrationBuilder.AlterColumn<string>(
                name: "expense_title",
                table: "AppExpenses",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
