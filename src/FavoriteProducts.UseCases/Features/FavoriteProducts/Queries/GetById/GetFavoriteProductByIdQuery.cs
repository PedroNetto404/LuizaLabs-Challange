using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetById;

public sealed record GetFavoriteProductByIdQuery(
    Guid CustomerId,
    Guid FavoriteProductId) : ICachedQuery<FavoriteProductDto>
{
    public string CacheKey => nameof(GetFavoriteProductByIdQuery);
    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}
