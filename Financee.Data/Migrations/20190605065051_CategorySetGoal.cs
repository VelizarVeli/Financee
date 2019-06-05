using Microsoft.EntityFrameworkCore.Migrations;

namespace Financee.Data.Migrations
{
    public partial class CategorySetGoal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SetGoal",
                table: "BudgetCategories",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SetGoal",
                table: "BudgetCategories");
        }
    }
}
