using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.ClearFavorites;

public sealed record ClearFavoritesCommand(
    Guid CustomerId
) : ICommand;
