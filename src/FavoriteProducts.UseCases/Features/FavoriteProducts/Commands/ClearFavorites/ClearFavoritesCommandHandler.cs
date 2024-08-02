using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.AcrossAggregateServices;
using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.ClearFavorites;

internal sealed class ClearFavoritesCommandHandler(
    FavoriteProductDomainService favoriteProductDomainService
) : ICommandHandler<ClearFavoritesCommand>
{
    public Task<Result> Handle(
        ClearFavoritesCommand request,
        CancellationToken cancellationToken) =>
        favoriteProductDomainService
            .ClearFavoritesAsync(
                request.CustomerId,
                cancellationToken);
}