using System;
using Ardalis.Specification;
using FavoriteProducts.Domain.Extensions;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;

public class GetAllFavoriteProductsSpecification : Specification<FavoriteProduct>
{
    public GetAllFavoriteProductsSpecification(Guid customerId, int page, int pageSize)
    {
        Query.Paginate(page, pageSize);
        Query.Where(x => x.CustomerId == customerId);
    }
}
