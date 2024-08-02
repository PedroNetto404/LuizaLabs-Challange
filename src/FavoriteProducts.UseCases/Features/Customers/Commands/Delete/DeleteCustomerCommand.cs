using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Delete;

public sealed record DeleteCustomerCommand(Guid CustomerId) : ICommand;
