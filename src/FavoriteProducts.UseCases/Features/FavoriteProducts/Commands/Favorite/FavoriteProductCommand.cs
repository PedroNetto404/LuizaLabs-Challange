using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Favorite;

public sealed record FavoriteProductCommand(
    Guid CustomerId,
    Guid ProductId
) : ICommand<FavoriteProductDto>;