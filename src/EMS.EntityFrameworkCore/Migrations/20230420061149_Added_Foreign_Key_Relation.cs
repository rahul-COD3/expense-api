using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EMS.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKeyRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_AppPayments_ExpenseId",
                table: "AppPayments",
                column: "ExpenseId");

            migrationBuilder.CreateIndex(
                name: "IX_AppPayments_OwnedBy",
                table: "AppPayments",
                column: "OwnedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_AppPayments_AbpUsers_OwnedBy",
                table: "AppPayments",
                column: "OwnedBy",
                principalTable: "AbpUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AppPayments_AppExpenses_ExpenseId",
                table: "AppPayments",
                column: "ExpenseId",
                principalTable: "AppExpenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppPayments_AbpUsers_OwnedBy",
                table: "AppPayments");

            migrationBuilder.DropForeignKey(
                name: "FK_AppPayments_AppExpenses_ExpenseId",
                table: "AppPayments");

            migrationBuilder.DropIndex(
                name: "IX_AppPayments_ExpenseId",
                table: "AppPayments");

            migrationBuilder.DropIndex(
                name: "IX_AppPayments_OwnedBy",
                table: "AppPayments");
        }
    }
}
