using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Products.ValueObjects;

public sealed record ProductReviewScore : ValueObject
{
    public const int MinValue = 0; // Product without reviews
    
    public int Value { get; }
    
    private ProductReviewScore(int value) =>
        Value = value;
    
    public static Result<ProductReviewScore> Create(int score) =>
        score < MinValue
        ? DomainErrors.Product.InvalidProductReviewScore
        : new ProductReviewScore(score);
}