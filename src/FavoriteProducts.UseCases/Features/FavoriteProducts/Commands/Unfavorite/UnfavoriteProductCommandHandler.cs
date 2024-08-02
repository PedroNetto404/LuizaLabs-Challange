using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.AcrossAggregateServices;
using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Unfavorite;

public sealed class UnfavoriteProductCommandHandler(
    FavoriteProductDomainService favoriteProductDomainService
) : ICommandHandler<UnfavoriteProductCommand>
{
    public Task<Result> Handle(UnfavoriteProductCommand request, CancellationToken cancellationToken) =>
        favoriteProductDomainService
            .UnfavoriteAsync(
                request.CustomerId,
                request.ProductId,
                cancellationToken);
}