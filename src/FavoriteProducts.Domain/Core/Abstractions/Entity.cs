namespace FavoriteProducts.Domain.Core.Abstractions;

public interface IEntity<TId> where TId : notnull, new()
{
    TId Id { get; }
}

public abstract class Entity<TId> :
    IEntity<TId>,
    IEquatable<Entity<TId>>
    where TId : notnull, new()
{
    public TId Id { get; } = new TId();

    public bool Equals(Entity<TId>? other) =>
        other is not null && Id.Equals(other.Id);

    public sealed override bool Equals(object? obj) =>
        obj is Entity<TId> other && Equals(other);

    public sealed override int GetHashCode() =>
        Id.GetHashCode();

    public static bool operator ==(Entity<TId>? left, Entity<TId>? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Entity<TId>? left, Entity<TId>? right) =>
        !(left == right);
}
