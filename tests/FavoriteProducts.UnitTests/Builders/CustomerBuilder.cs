using System.Reflection;
using Bogus;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;

namespace FavoriteProducts.UnitTests.Builders;

public sealed class CustomerBuilder
{
    private static readonly Faker _faker = new();
    private CustomerName _name = CustomerName.Create(_faker.Name.FullName()).Value;
    private Email _email = Email.Create(_faker.Internet.Email()).Value;

    public CustomerBuilder WithName(CustomerName name)
    {
        _name = name;
        return this;
    }

    public CustomerBuilder WithEmail(Email email)
    {
        _email = email;
        return this;
    }


    public Customer Build()
    {
        var customer = new Customer(_name, _email);
        return customer;
    }
}