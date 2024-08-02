using Ardalis.Specification;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;

public sealed class FavoriteProductsByCustomerSpecification : Specification<FavoriteProduct>
{
    public FavoriteProductsByCustomerSpecification(Guid customerId) =>
        Query.Where(favoriteProduct => favoriteProduct.CustomerId == customerId);
}