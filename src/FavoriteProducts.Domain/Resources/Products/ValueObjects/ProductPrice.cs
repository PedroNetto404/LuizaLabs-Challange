using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Products.ValueObjects;

public sealed record ProductPrice : ValueObject
{
    public const decimal MinValue = 0.01m;
    public decimal Value { get; }

    private ProductPrice(decimal value) =>
        Value = value;

    public static Result<ProductPrice> Create(decimal price) =>
        price <= 0
        ? DomainErrors.Product.InvalidProductPrice
        : new ProductPrice(price);

    public static implicit operator decimal(ProductPrice price) => price.Value;

    public static implicit operator ProductPrice(decimal price) => new(price);

    public override string ToString() => Value.ToString("C");

    public static decimal operator +(ProductPrice price1, ProductPrice price2) => price1.Value + price2.Value;

    public static decimal operator -(ProductPrice price1, ProductPrice price2) => price1.Value - price2.Value;

    public static bool operator <(ProductPrice price1, ProductPrice price2) => price1.Value < price2.Value;

    public static bool operator >(ProductPrice price1, ProductPrice price2) => price1.Value > price2.Value;

    public static bool operator <=(ProductPrice price1, ProductPrice price2) => price1.Value <= price2.Value;

    public static bool operator >=(ProductPrice price1, ProductPrice price2) => price1.Value >= price2.Value;
}
