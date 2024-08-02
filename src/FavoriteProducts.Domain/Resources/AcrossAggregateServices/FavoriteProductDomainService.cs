using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.Domain.Resources.Products;

namespace FavoriteProducts.Domain.Resources.AcrossAggregateServices;

public sealed class FavoriteProductDomainService(
    IRepository<FavoriteProduct> favoriteProductRepository,
    IRepository<Product> productRepository,
    IRepository<Customer> customerRepository,
    IUnitOfWork unitOfWork
)
{
    public async Task<Result> ClearFavoritesAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null)
        {
            return DomainErrors.Customer.NotFound;
        }

        var favoriteProducts = await favoriteProductRepository.GetManyAsync(
            new FavoriteProductsByCustomerSpecification(customerId),
            cancellationToken);
        if (favoriteProducts.Any() is false)
        {
            return Result.Ok();
        }

        await Task.WhenAll(
            favoriteProducts.Select(favoriteProductRepository.DeleteAsync)
        );

        return await unitOfWork.CommitAsync(cancellationToken) is true ?
            Result.Ok() :
            DomainErrors.FavoriteProduct.NotDeleted;
    }

    public async Task<Result<FavoriteProduct>> FavoriteAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var customer = await customerRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null)
        {
            return DomainErrors.Customer.NotFound;
        }

        var product = await productRepository.GetByIdAsync(productId, cancellationToken);
        if (product is null)
        {
            return DomainErrors.Product.NotFound;
        }

        if (product.Active is false)
        {
            return DomainErrors.FavoriteProduct.CannotFavoriteInactiveProduct;
        }

        var favoriteProduct = new FavoriteProduct(
            customer.Id, 
            product.Id,
            product.Title.Value);

        await favoriteProductRepository.AddAsync(favoriteProduct);
        return await unitOfWork.CommitAsync(cancellationToken) is true 
        ? favoriteProduct 
        : DomainErrors.FavoriteProduct.NotSaved;
    }

    public async Task<Result> UnfavoriteAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var favoriteProduct = await favoriteProductRepository.GetOneAsync(
            new FavoriteProductByCustomerAndProductSpecification(
                customerId,
                productId),
            cancellationToken);
        if (favoriteProduct is null)
        {
            return DomainErrors.FavoriteProduct.NotFound;
        }

        await favoriteProductRepository.DeleteAsync(favoriteProduct);
        return await unitOfWork.CommitAsync(cancellationToken) is true ?
            Result.Ok() :
            DomainErrors.FavoriteProduct.NotDeleted;
    }
}