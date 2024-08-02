using FavoriteProducts.Domain.Core.Abstractions;

namespace FavoriteProducts.Domain.Resources.FavoriteProducts;

public sealed class FavoriteProduct : Entity, IAuditableEntity
{
    public FavoriteProduct(Guid customerId, Guid productId) =>
     (CustomerId, ProductId) = (customerId, productId);

    public Guid CustomerId { get; private set; }
    public Guid ProductId { get; private set; }
    public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    public DateTime ModifiedAtUtc { get;  set; } = DateTime.UtcNow;
    public DateTime? DeletedAtUtc { get;  set; } = DateTime.UtcNow;

#pragma warning disable CS0628 // New protected member declared in sealed type
    protected FavoriteProduct() { }
#pragma warning restore CS0628 // New protected member declared in sealed type
}
