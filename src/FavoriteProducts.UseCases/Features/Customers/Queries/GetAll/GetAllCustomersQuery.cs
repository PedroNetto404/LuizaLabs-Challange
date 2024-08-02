using FavoriteProducts.Domain.Shared.Enums;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Queries.GetAll;

public sealed record GetAllCustomersQuery(
    string SortBy,
    SortOrder SortOrder,
    int Page,
    int PageSize) :
    ICachedQuery<IEnumerable<CustomerDto>>
{
    public string CacheKey => nameof(GetAllCustomersQuery);

    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}
