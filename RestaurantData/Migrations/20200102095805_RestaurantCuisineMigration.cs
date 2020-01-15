using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantData.Migrations
{
    public partial class RestaurantCuisineMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantCuisine",
                table: "RestaurantCuisine");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "RestaurantCuisine",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantCuisine",
                table: "RestaurantCuisine",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCuisine_restaurantID",
                table: "RestaurantCuisine",
                column: "restaurantID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RestaurantCuisine",
                table: "RestaurantCuisine");

            migrationBuilder.DropIndex(
                name: "IX_RestaurantCuisine_restaurantID",
                table: "RestaurantCuisine");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "RestaurantCuisine");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RestaurantCuisine",
                table: "RestaurantCuisine",
                columns: new[] { "restaurantID", "cuisineTypeID" });
        }
    }
}
