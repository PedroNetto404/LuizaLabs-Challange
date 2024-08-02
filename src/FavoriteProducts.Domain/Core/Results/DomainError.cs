using FavoriteProducts.Domain.Core.Abstractions;

namespace FavoriteProducts.Domain.Core.Results;

public sealed record DomainError<TEntity>(string Code, string Message) : 
    Error(Code, Message),
    IDomainError
    where TEntity : IEntity
{
    public string ResourceName => typeof(TEntity).Name;
}

/// <summary>
/// Markup interface for generic constraints.
/// </summary>
public interface IDomainError;