using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Create;

public sealed class CreateProductCommandHandler(
    IRepository<Product> productRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProductCommand, ProductDto>
{
    public async Task<Result<ProductDto>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var maybeFields = TryBuildFields(request);
        if (maybeFields.IsFailure)
        {
            return maybeFields.Error;
        }

        var (
            maybeTitle,
            maybeBrand,
            maybeDescription,
            maybePrice,
            maybeReviewScore
            ) = maybeFields.Value;

        var maybeProduct = Product.Create(
            maybeTitle,
            maybeDescription,
            maybePrice,
            maybeBrand,
            maybeReviewScore,
            request.ImageUrl);
        if (maybeProduct.IsFailure)
        {
            return maybeProduct.Error;
        }

        await productRepository.AddAsync(maybeProduct.Value);
        return await unitOfWork.CommitAsync(cancellationToken) is true
            ? ProductDto.FromEntity(maybeProduct.Value)
            : DomainErrors.Product.CreateProductFailed;
    }

    private static Result<(ProductTitle, ProductBrand, ProductDescription, ProductPrice, ProductReviewScore)>
        TryBuildFields(
            CreateProductCommand request)
    {
        var maybeTitle = ProductTitle.Create(request.Title);
        if (maybeTitle.IsFailure)
        {
            return maybeTitle.Error;
        }

        var maybeBrand = ProductBrand.Create(request.Brand);
        if (maybeBrand.IsFailure)
        {
            return maybeBrand.Error;
        }

        var maybeDescription = ProductDescription.Create(request.Description);
        if (maybeDescription.IsFailure)
        {
            return maybeDescription.Error;
        }

        var maybePrice = ProductPrice.Create(request.Price);
        if (maybePrice.IsFailure)
        {
            return maybePrice.Error;
        }

        var maybeReviewScore = ProductReviewScore.Create(request.ReviewScore);
        if (maybeReviewScore.IsFailure)
        {
            return maybeReviewScore.Error;
        }

        return (
            maybeTitle.Value,
            maybeBrand.Value,
            maybeDescription.Value,
            maybePrice.Value,
            maybeReviewScore.Value
        );
    }
}