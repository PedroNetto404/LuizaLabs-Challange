using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Create;

public record class CreateCustomerCommand(
    string Name,
    string Email
) : ICommand<CustomerDto>;
