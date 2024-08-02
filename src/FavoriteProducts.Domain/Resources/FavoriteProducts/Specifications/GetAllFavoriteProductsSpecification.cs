using System;
using Ardalis.Specification;
using FavoriteProducts.Domain.Extensions;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;

public sealed class GetAllFavoriteProductsSpecification : Specification<FavoriteProduct>
{
    public GetAllFavoriteProductsSpecification(
        Guid customerId, int page, int pageSize)
    {
        Query.Where(p => p.CustomerId == customerId)
             .Paginate(page, pageSize); 
    }
}
