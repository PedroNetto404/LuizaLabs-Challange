namespace FavoriteProducts.Domain.Core.Abstractions;

public abstract class Entity :
    IEntity,
    IEquatable<Entity>
{
    public Guid Id { get; } = Guid.NewGuid();

    public bool Equals(Entity? other) =>
        other is not null && Id.Equals(other.Id);

    public sealed override bool Equals(object? obj) =>
        obj is Entity other && Equals(other);

    public sealed override int GetHashCode() =>
        Id.GetHashCode();

    public static bool operator ==(Entity? left, Entity? right) =>
        left?.Equals(right) ?? right is null;

    public static bool operator !=(Entity? left, Entity? right) =>
        !(left == right);
}
