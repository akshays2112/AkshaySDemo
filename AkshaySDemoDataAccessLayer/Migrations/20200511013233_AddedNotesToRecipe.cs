using Microsoft.EntityFrameworkCore.Migrations;

namespace AkshaySDemo.Migrations
{
    public partial class AddedNotesToRecipe : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Recipes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Recipes");
        }
    }
}
