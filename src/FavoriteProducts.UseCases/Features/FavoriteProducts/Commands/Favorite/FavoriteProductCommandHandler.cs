using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.AcrossAggregateServices;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Favorite;

internal sealed class FavoriteProductCommandHandler(
    FavoriteProductDomainService favoriteProductDomainService
) : ICommandHandler<FavoriteProductCommand, FavoriteProductDto>
{
    public Task<Result<FavoriteProductDto>> Handle(FavoriteProductCommand request, CancellationToken cancellationToken) =>
        favoriteProductDomainService
            .FavoriteAsync(
                request.CustomerId,
                request.ProductId,
                cancellationToken)
            .MapAsync(FavoriteProductDto.FromEntity);
         
}

