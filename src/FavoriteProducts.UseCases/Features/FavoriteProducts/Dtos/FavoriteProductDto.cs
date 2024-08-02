using FavoriteProducts.Domain.Resources.FavoriteProducts;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

public sealed record FavoriteProductDto(
    Guid Id,
    Guid CustomerId, 
    Guid ProductId, 
    string ProductTitle,
    DateTime CreatedAtUtc)
{
    public static FavoriteProductDto FromEntity(FavoriteProduct favoriteProduct) =>
        new(
            favoriteProduct.Id,
            favoriteProduct.CustomerId,
            favoriteProduct.ProductId,
            favoriteProduct.ProductTitle,
            favoriteProduct.CreatedAtUtc);
}