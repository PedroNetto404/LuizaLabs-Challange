using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Delete;

public sealed class DeleteProductCommandHandler(
    IRepository<Product> productRepository,
    IUnitOfWork unitOfWork
) : ICommandHandler<DeleteProductCommand>
{
    public async Task<Result> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
        {
            return DomainErrors.Product.NotFound;
        }

        await productRepository.DeleteAsync(product);
        return await unitOfWork.CommitAsync(cancellationToken) is true
            ? Result.Ok()
            : DomainErrors.Product.DeleteProductFailed;
    }
}