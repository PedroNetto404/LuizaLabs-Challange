using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetById;

public sealed class GetFavoriteProductByIdQueryHandler(IRepository<FavoriteProduct> favoriteProductRepository) : 
    IQueryHandler<GetFavoriteProductByIdQuery, FavoriteProductDto>
{
    public async Task<Result<FavoriteProductDto>> Handle(GetFavoriteProductByIdQuery request, CancellationToken cancellationToken)
    {
        var favoriteProduct = await favoriteProductRepository.GetByIdAsync(request.FavoriteProductId, cancellationToken);
        if(favoriteProduct is null || favoriteProduct.CustomerId != request.CustomerId)
        {
            return DomainErrors.FavoriteProduct.NotFound;
        }

        return FavoriteProductDto.FromEntity(favoriteProduct);
    }
}