using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Customers.ValueObjects;

public sealed record CustomerName
{
    public const int MaxLength = 100;
    public const int MinLength = 3;

    public string Value { get; }

    private CustomerName(string value) => Value = value;

    public static Result<CustomerName> Create(string name)
    {
        if (
            string.IsNullOrEmpty(name) ||
            name.Length is < MinLength or > MaxLength)
        {
            return DomainErrors.Customer.InvalidCustomerName;
        }

        return new CustomerName(name);
    }
}