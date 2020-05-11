using Microsoft.EntityFrameworkCore.Migrations;

namespace AkshaySDemo.Migrations
{
    public partial class ChangedTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_recipes",
                table: "recipes");

            migrationBuilder.RenameTable(
                name: "recipes",
                newName: "Recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Recipes",
                table: "Recipes");

            migrationBuilder.RenameTable(
                name: "Recipes",
                newName: "recipes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_recipes",
                table: "recipes",
                column: "ID");
        }
    }
}
