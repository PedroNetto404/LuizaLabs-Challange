using System.Linq.Expressions;
using System.Reflection;
using Ardalis.Specification;
using FavoriteProducts.Domain.Extensions;
using FavoriteProducts.Domain.Shared.Enums;

namespace FavoriteProducts.Domain.Resources.Customers.Specifications;

public sealed class GetAllCustomersSpecification : Specification<Customer>
{
    private static readonly IDictionary<string, Expression<Func<Customer, object?>>> AllowedSortValues = new Dictionary<string, Expression<Func<Customer, object?>>>
    {
        {nameof(Customer.Name), p => p.Name},
        {nameof(Customer.Email), p => p.Email},
        {nameof(Customer.Id), p => p.Id}
    };

    public GetAllCustomersSpecification(
        int page,
        int pageSize,
        string sortBy,
        SortOrder sortOrder)
    {
        Query.Paginate(page, pageSize)
             .SortBy(sortBy, sortOrder, AllowedSortValues);
    }   
}