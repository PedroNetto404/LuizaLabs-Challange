using System.Reflection;
using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Update;

public sealed class UpdateProductCommandHandler(
    IRepository<Product> productRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateProductCommand, ProductDto>
{

    public async Task<Result<ProductDto>> Handle(
        UpdateProductCommand command,
        CancellationToken cancellationToken)
    {
        command.Deconstruct(
            out Guid productId,
            out string title,
            out string brand,
            out string description,
            out decimal price,
            out string imageUrl,
            out bool active);

        var product = await productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            return DomainErrors.Product.NotFound;
        }

        if (active)
        {
            product.Activate();
        }
        else
        {
            product.Deactivate();
        }

        var maybeErrors = new List<Result>
        {
            product.UpdateTitle(title),
            product.UpdateDescription(description),
            product.UpdateBrand(brand),
            product.UpdatePrice(price),
            product.UpdateImageUrl(imageUrl)
        }
        .Where(p => p.IsFailure)
        .Select(p => p.Error);

        if (maybeErrors.Any())
        {
            return Error.MultipleErrors(maybeErrors);
        }

        await productRepository.UpdateAsync(product);
        return await unitOfWork.CommitAsync(cancellationToken) is true
            ? ProductDto.FromEntity(product)
            : DomainErrors.Product.UpdateProductFailed;
    }
}
