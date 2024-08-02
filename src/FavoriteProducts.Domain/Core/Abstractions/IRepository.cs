using Ardalis.Specification;

namespace FavoriteProducts.Domain.Core.Abstractions;

public interface IRepository<TEntity> where TEntity : IEntity
{
    Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<List<TEntity>> GetManyAsync(ISpecification<TEntity>? specification = null, CancellationToken cancellationToken = default);
    Task<TEntity?> GetOneAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
