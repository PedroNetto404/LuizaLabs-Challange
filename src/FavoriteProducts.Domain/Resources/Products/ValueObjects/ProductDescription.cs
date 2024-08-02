using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Products.ValueObjects;

public sealed record ProductDescription : ValueObject
{
    public const int MaxLength = 1000;
    public const int MinLength = 5;

    public string Value { get; }

    private ProductDescription(string value) =>
        Value = value;

    public static Result<ProductDescription> Create(string description)
    {
        if (
            string.IsNullOrWhiteSpace(description) || 
            description.Length is < MinLength or > MaxLength
        )
        {
            return DomainErrors.Product.InvalidProductDescription;
        }
     
        return new ProductDescription(description);
    }
}
