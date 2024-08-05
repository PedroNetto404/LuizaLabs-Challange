using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.Specifications;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Create;

public sealed class CreateProductCommandHandler(
    IRepository<Product> productRepository) : ICommandHandler<CreateProductCommand, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {   
        var maybeTitle = ProductTitle.Create(request.Title);
        var maybeDescription = ProductDescription.Create(request.Description);
        var maybePrice = ProductPrice.Create(request.Price);
        var maybeBrand = ProductBrand.Create(request.Brand);
        var maybeReviewScore = ProductReviewScore.Create(request.ReviewScore);
        var composed = new List<Result>
        {
            maybeTitle,
            maybeDescription,
            maybePrice,
            maybeBrand,
            maybeReviewScore
        };
        if (composed.Any(result => result.IsFailure))
        {
            return Error.MultipleErrors(composed.Select(result => result.Error));
        }

        var existingProduct = await productRepository.SingleOrDefaultAsync(
            new GetProductByTitleSpecification(maybeTitle.Value), cancellationToken);
            
        var maybeProduct = Product.Create(
            maybeTitle.Value,
            maybeDescription.Value,
            maybePrice.Value,
            maybeBrand.Value,
            maybeReviewScore.Value,
            request.ImageUrl);
        if (maybeProduct.IsFailure)
        {
            return maybeProduct.Error;
        }

        var product = await productRepository.AddAsync(maybeProduct.Value, cancellationToken);
        return ProductDto.FromEntity(product);
    }
}