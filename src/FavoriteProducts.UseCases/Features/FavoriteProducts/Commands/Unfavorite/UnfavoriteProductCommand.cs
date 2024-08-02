using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Unfavorite;

public sealed record UnfavoriteProductCommand(
    Guid CustomerId,
    Guid ProductId
) : ICommand;
