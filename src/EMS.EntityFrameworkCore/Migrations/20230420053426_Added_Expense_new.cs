using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedExpensenew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterId",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "currency",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "expense_amount",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "expense_description",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "group_id",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "paid_by",
                table: "AppExpenses");

            migrationBuilder.DropColumn(
                name: "split_as",
                table: "AppExpenses");

            migrationBuilder.RenameColumn(
                name: "expense_title",
                table: "AppExpenses",
                newName: "ExpenseTitle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExpenseTitle",
                table: "AppExpenses",
                newName: "expense_title");

            migrationBuilder.AddColumn<Guid>(
                name: "DeleterId",
                table: "AppExpenses",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "AppExpenses",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AppExpenses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "currency",
                table: "AppExpenses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "expense_amount",
                table: "AppExpenses",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "expense_description",
                table: "AppExpenses",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "group_id",
                table: "AppExpenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "paid_by",
                table: "AppExpenses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "split_as",
                table: "AppExpenses",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
