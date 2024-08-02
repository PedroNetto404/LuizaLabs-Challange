using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Update;

public sealed record UpdateCustomerCommand(
    Guid CustomerId,
    string Name,
    string Email
) : ICommand<CustomerDto>;