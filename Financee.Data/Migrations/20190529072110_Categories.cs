using Microsoft.EntityFrameworkCore.Migrations;

namespace Financee.Data.Migrations
{
    public partial class Categories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_BudgetCategory_BudgetCategoryId",
                table: "Expenditures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetCategory",
                table: "BudgetCategory");

            migrationBuilder.RenameTable(
                name: "BudgetCategory",
                newName: "BudgetCategories");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetCategories",
                table: "BudgetCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_BudgetCategories_BudgetCategoryId",
                table: "Expenditures",
                column: "BudgetCategoryId",
                principalTable: "BudgetCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expenditures_BudgetCategories_BudgetCategoryId",
                table: "Expenditures");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BudgetCategories",
                table: "BudgetCategories");

            migrationBuilder.RenameTable(
                name: "BudgetCategories",
                newName: "BudgetCategory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BudgetCategory",
                table: "BudgetCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Expenditures_BudgetCategory_BudgetCategoryId",
                table: "Expenditures",
                column: "BudgetCategoryId",
                principalTable: "BudgetCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
