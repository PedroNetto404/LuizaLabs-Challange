using FavoriteProducts.Domain.Core.Abstractions;

namespace FavoriteProducts.Domain.Core.Results;

public sealed record DomainResult<TEntity>(string Code, string Message) : 
    Error(Code, Message)
    where TEntity : IEntity
{
    public string ResourceName => typeof(TEntity).Name;
}
