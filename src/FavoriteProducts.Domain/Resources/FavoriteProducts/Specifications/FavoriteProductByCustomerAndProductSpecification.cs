using Ardalis.Specification;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;

public sealed class FavoriteProductByCustomerAndProductSpecification : SingleResultSpecification<FavoriteProduct>
{
    public FavoriteProductByCustomerAndProductSpecification(
        Guid customerId,
        Guid productId) =>
        Query
            .Where(favoriteProduct => favoriteProduct.CustomerId == customerId)
            .Where(favoriteProduct => favoriteProduct.ProductId == productId)
            .Where(favoriteProduct => favoriteProduct.DeletedAtUtc == null);
}