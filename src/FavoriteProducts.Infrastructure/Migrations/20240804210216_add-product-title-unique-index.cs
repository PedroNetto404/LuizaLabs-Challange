using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteProducts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addproducttitleuniqueindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_products_title",
                table: "products",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_products_title",
                table: "products");
        }
    }
}
