using FavoriteProducts.Domain.Core.Abstractions;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts;

public sealed class FavoriteProduct : Entity, IAuditableEntity
{
    public FavoriteProduct(
        Guid customerId,
        Guid productId,
        string productTitle) =>
        (CustomerId, ProductId, ProductTitle) = (customerId, productId, productTitle);
    public Guid CustomerId { get; private set; }
    public string ProductTitle { get; private set; }
    public Guid ProductId { get; private set; }
    public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    public DateTime ModifiedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAtUtc { get; set; }

#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected FavoriteProduct() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS0628 // New protected member declared in sealed type
}
