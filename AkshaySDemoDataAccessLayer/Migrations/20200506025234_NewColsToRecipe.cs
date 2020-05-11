using Microsoft.EntityFrameworkCore.Migrations;

namespace AkshaySDemo.Migrations
{
    public partial class NewColsToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Recipes",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatorsName",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "CreatorsName",
                table: "Recipes");
        }
    }
}
