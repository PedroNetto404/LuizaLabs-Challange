using Ardalis.Specification;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;

public sealed class FavoriteProductByCustomerAndProductSpecification : Specification<FavoriteProduct>
{
    public FavoriteProductByCustomerAndProductSpecification(
        Guid customerId,
        Guid productId) =>
        Query.Where(x =>
            x.CustomerId == customerId &&
            x.ProductId == productId);
}