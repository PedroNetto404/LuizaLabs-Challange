using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using FavoriteProducts.Domain.Extensions;
using FavoriteProducts.Domain.Shared.Enums;

namespace FavoriteProducts.Domain.Resources.Products.Specifications;

public class GetAllProductsSpecification : Specification<Product>
{
    public static readonly IDictionary<string, Expression<Func<Product, object?>>> AllowedSortValues = new Dictionary<string, Expression<Func<Product, object?>>>
    {
        {nameof(Product.Title), p => p.Title},
        {nameof(Product.Price), p => p.Price},
        {nameof(Product.Brand), p => p.Brand},
        {nameof(Product.Id), p => p.Id}
    };

    public GetAllProductsSpecification(
        int page,
        int pageSize,
        string sortBy,
        SortOrder sortOrder
    )   
    {   
        Query.Paginate(page, pageSize)
             .SortBy(sortBy, sortOrder, AllowedSortValues);
    }
}
