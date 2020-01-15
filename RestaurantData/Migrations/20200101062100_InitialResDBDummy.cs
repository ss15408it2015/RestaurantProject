using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantData.Migrations
{
    public partial class InitialResDBDummy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CuisineTypes",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cuisineType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CuisineTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(nullable: true),
                    street = table.Column<string>(nullable: true),
                    locality = table.Column<string>(nullable: true),
                    city = table.Column<string>(nullable: true),
                    state = table.Column<string>(nullable: true),
                    country = table.Column<string>(nullable: true),
                    postal = table.Column<string>(nullable: true),
                    lat = table.Column<float>(nullable: false),
                    lng = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rating = table.Column<int>(nullable: false),
                    restaurantID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ratings_Restaurants_restaurantID",
                        column: x => x.restaurantID,
                        principalTable: "Restaurants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RestaurantCuisine",
                columns: table => new
                {
                    restaurantID = table.Column<int>(nullable: false),
                    cuisineTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RestaurantCuisine", x => new { x.restaurantID, x.cuisineTypeID });
                    table.ForeignKey(
                        name: "FK_RestaurantCuisine_CuisineTypes_cuisineTypeID",
                        column: x => x.cuisineTypeID,
                        principalTable: "CuisineTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RestaurantCuisine_Restaurants_restaurantID",
                        column: x => x.restaurantID,
                        principalTable: "Restaurants",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_restaurantID",
                table: "Ratings",
                column: "restaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_RestaurantCuisine_cuisineTypeID",
                table: "RestaurantCuisine",
                column: "cuisineTypeID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "RestaurantCuisine");

            migrationBuilder.DropTable(
                name: "CuisineTypes");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
