using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteProducts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addproducttitletofavoritedproject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "product_title",
                table: "favorite_products",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "product_title",
                table: "favorite_products");
        }
    }
}
