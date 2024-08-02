using FavoriteProducts.Domain.Resources.Customers;

namespace FavoriteProducts.UseCases.Features.Customers.Dtos;

public sealed record CustomerDto(
    Guid Id,
    string Name,
    string Email)
{
    public static CustomerDto FromEntity(Customer customer) =>
        new(
            customer.Id,
            customer.Name.Value,
            customer.Email.Value);
}