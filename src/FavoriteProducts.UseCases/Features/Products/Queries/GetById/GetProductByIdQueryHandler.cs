using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Queries.GetById;

public sealed class GetProductByIdQueryHandler(IRepository<Product> productRepository) : 
    IQueryHandler<GetProductByIdQuery, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken) =>
        await productRepository.GetByIdAsync(request.ProductId, cancellationToken) is not {} product
            ? DomainErrors.Product.NotFound
            : ProductDto.FromEntity(product);
}