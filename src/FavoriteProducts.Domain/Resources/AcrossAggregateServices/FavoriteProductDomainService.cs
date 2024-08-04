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
    IRepository<Customer> customerRepository
)
{
    public async Task<Result> ClearFavoritesAsync(Guid customerId, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(customerId, cancellationToken);
        if (customer is null)
        {
            return DomainErrors.Customer.NotFound;
        }

        var favoriteProducts = await favoriteProductRepository.ListAsync(
            new FavoriteProductsByCustomerSpecification(customerId),
            cancellationToken);
        if (favoriteProducts.Count != 0)
        {
            return Result.Ok();
        }

        await favoriteProductRepository.DeleteRangeAsync(favoriteProducts, cancellationToken);
        return Result.Ok();
    }

    public async Task<Result<FavoriteProduct>> FavoriteAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var existingFavoriteProduct = await favoriteProductRepository.SingleOrDefaultAsync(
            new FavoriteProductByCustomerAndProductSpecification(
                customerId,
                productId),
            cancellationToken);
        if (existingFavoriteProduct is not null)
        {
            return DomainErrors.FavoriteProduct.AlreadyFavorite;
        }

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

        var favoriteProduct = await favoriteProductRepository.AddAsync(
            new FavoriteProduct(
            customer.Id,
            product.Id,
            product.Title.Value), cancellationToken);
        return favoriteProduct is not null
            ? Result.Ok(favoriteProduct)
            : DomainErrors.FavoriteProduct.NotSaved;
    }

    public async Task<Result> UnfavoriteAsync(
        Guid customerId,
        Guid productId,
        CancellationToken cancellationToken = default)
    {
        var favoriteProduct = await favoriteProductRepository.FirstOrDefaultAsync(
            new FavoriteProductByCustomerAndProductSpecification(
                customerId,
                productId),
            cancellationToken);
        if (favoriteProduct is null)
        {
            return DomainErrors.FavoriteProduct.NotFound;
        }

        await favoriteProductRepository.DeleteAsync(favoriteProduct, cancellationToken);
        return Result.Ok();
    }
}