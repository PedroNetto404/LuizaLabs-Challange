using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetAll;

public sealed record GetAllFavoriteProductsQuery(Guid CustomerId, int Page, int PageSize) : ICachedQuery<IEnumerable<FavoriteProductDto>>
{
    public string CacheKey => nameof(GetAllFavoriteProductsQuery);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}