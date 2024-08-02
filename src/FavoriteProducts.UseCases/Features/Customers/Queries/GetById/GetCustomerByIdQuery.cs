using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Queries.GetById;

public sealed record GetCustomerByIdQuery(Guid CustomerId) : ICachedQuery<CustomerDto>
{
    public string CacheKey => nameof(GetCustomerByIdQuery);
    public TimeSpan? Expiration => TimeSpan.FromMinutes(1);
}
