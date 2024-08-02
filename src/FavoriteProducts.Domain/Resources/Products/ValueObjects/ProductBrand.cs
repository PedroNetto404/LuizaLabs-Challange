using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Products.ValueObjects;

public sealed record ProductBrand
{
    public const int MaxLength = 100;
    public const int MinLength = 3;

    public string Value { get; }

    private ProductBrand(string value) =>
        Value = value;

    public static Result<ProductBrand> Create(string brand)
    {
        if (
            string.IsNullOrWhiteSpace(brand) || 
            brand.Length is < MinLength or > MaxLength
        )
            return DomainErrors.Product.InvalidProductBrand;
     
        return new ProductBrand(brand);
    }
}
