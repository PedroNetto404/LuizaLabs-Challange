using Ardalis.Specification;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;

namespace FavoriteProducts.Domain.Resources.Customers.Specifications;

public sealed class GetCustomerByEmailSpecification : Specification<Customer>
{
    public GetCustomerByEmailSpecification(Email email) => 
        Query.Where(customer => customer.Email == email);
} 