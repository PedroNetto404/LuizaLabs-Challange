using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Queries.GetById;

public sealed record GetProductByIdQuery(Guid ProductId) : ICachedQuery<ProductDto>
{
    public string CacheKey => nameof(GetProductByIdQuery);
    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}