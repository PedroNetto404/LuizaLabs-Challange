using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetAll;

public sealed class GetAllFavoriteProductsQueryHandler(
    IRepository<FavoriteProduct> favoriteProductRepository
) : IQueryHandler<GetAllFavoriteProductsQuery, IEnumerable<FavoriteProductDto>>
{
    public async Task<Result<IEnumerable<FavoriteProductDto>>> Handle(
        GetAllFavoriteProductsQuery request, 
        CancellationToken cancellationToken)
    {
        var favoriteProducts = await favoriteProductRepository.GetManyAsync(
            new GetAllFavoriteProductsSpecification(
                request.CustomerId, 
                request.Page, 
                request.PageSize), 
            cancellationToken);

        return Result.Ok(favoriteProducts.Select(FavoriteProductDto.FromEntity));
    }
        
}