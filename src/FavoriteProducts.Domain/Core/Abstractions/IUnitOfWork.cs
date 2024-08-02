namespace FavoriteProducts.Domain.Core.Abstractions;

public interface IUnitOfWork
{   
    Task<bool> CommitAsync(CancellationToken cancellationToken = default);
}