using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FavoriteProducts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addreviewscoretoproduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "review_score",
                table: "products",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "review_score",
                table: "products");
        }
    }
}
