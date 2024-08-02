using FavoriteProducts.Domain.Shared.Enums;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Queries.GetAll;

public sealed record GetAllProductsQuery(
    string SortBy,
    SortOrder SortOrder,
    bool Active,
    int Page, 
    int PageSize) : ICachedQuery<IEnumerable<ProductDto>>
{
    public string CacheKey => nameof(GetAllProductsQuery);
    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}   