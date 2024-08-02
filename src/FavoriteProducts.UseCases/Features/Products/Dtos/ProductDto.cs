using FavoriteProducts.Domain.Resources.Products;

namespace FavoriteProducts.UseCases.Features.Products.Dtos;

public sealed record ProductDto(
    Guid Id,
    string Title,
    string Brand,
    string Description,
    decimal Price,
    string ImageUrl,
    bool Active,
    DateTime CreatedAtUtc
)
{
    public static ProductDto FromEntity(Product product) =>
        new(
            product.Id,
            product.Title.Value,
            product.Brand.Value,
            product.Description.Value,
            product.Price.Value,
            product.ImageUrl,
            product.Active,
            product.CreatedAtUtc
        );
}