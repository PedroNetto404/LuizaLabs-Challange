using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.Specifications;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Queries.GetAll;

public sealed class GetAllProductsQueryHandler(
    IRepository<Product> repository
) : IQueryHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    public async Task<Result<IEnumerable<ProductDto>>> Handle(
        GetAllProductsQuery request, 
        CancellationToken cancellationToken) 
    {
        var result = await repository.ListAsync(new GetAllProductsSpecification(
            request.Page,
            request.PageSize,
            request.SortBy,
            request.SortOrder
        ), cancellationToken);

        return Result.Ok(result.Select(ProductDto.FromEntity));
    }
        
}