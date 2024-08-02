using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Products.ValueObjects;

public sealed record ProductTitle
{
    public const int MaxLength = 100;
    public const int MinLength = 3;

    public string Value { get; }

    private ProductTitle(string value) =>
        Value = value;

    public static Result<ProductTitle> Create(string title)
    {
        if (
            string.IsNullOrWhiteSpace(title) || 
            title.Length is < MinLength or > MaxLength
        )
        {
            return DomainErrors.Product.InvalidProductTitle;
        }
        
        return new ProductTitle(title);
    }
}
