using System.Reflection;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;

namespace FavoriteProducts.UnitTests.Builders;

public sealed class CustomerBuilder
{
    private CustomerName _name = CustomerName.Create("Pedro Netto").Value;
    private Email _email = Email.Create("pedronetto31415@gmail.com").Value;

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