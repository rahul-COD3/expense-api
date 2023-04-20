using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpenseNewEnitiyAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyType",
                table: "AppExpenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "ExpenseAmount",
                table: "AppExpenses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ExpenseDescription",
                table: "AppExpenses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsSettled",
                table: "AppExpenses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SplitType",
                table: "AppExpenses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "groupId",
                table: "AppExpenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "paidBy",
                table: "AppExpenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

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
                name: "CurrencyType",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "ExpenseAmount",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "ExpenseDescription",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "IsSettled",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "SplitType",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "groupId",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "paidBy",
                table: "AppExpenses");
        }
    }
}
