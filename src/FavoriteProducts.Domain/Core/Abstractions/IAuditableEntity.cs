namespace FavoriteProducts.Domain.Core.Abstractions;

public interface IAuditableEntity : IEntity
{
    DateTime CreatedAtUtc { get; }
    DateTime ModifiedAtUtc { get;  internal set; }
    DateTime? DeletedAtUtc { get;  internal set; }

    void OnModify() => ModifiedAtUtc = DateTime.UtcNow;

    void OnDelete() => DeletedAtUtc = DateTime.UtcNow;
}