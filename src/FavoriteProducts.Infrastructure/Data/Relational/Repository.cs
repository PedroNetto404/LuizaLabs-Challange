using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FavoriteProducts.Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Infrastructure.Data.Relational;

internal sealed class Repository<TEntity>(FavoriteProductsContext context)
    : IRepository<TEntity> where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> entities = context.Set<TEntity>();
    private readonly SpecificationEvaluator evaluator = SpecificationEvaluator.Default;

    public async Task<List<TEntity>> GetManyAsync(
        ISpecification<TEntity>? specification = null,
        CancellationToken cancellationToken = default)
    {
        var query = entities.AsQueryable();
        if (specification is not null)
        {
            query = evaluator.GetQuery(query, specification);
        }

        return await query.ToListAsync(cancellationToken);
    }

    public Task<TEntity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        entities
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);


    public Task<TEntity?> GetOneAsync(ISpecification<TEntity> specification,
        CancellationToken cancellationToken = default) =>
        evaluator
            .GetQuery(entities.AsQueryable(), specification)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

    public Task AddAsync(TEntity entity)
    {
        entities.Add(entity);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(TEntity entity)
    {
        if (entity is IAuditableEntity auditableEntity)
        {
            context.Entry(entity).State = EntityState.Modified;
            auditableEntity.OnDelete();
            return UpdateAsync(entity);
        }

        entities.Remove(entity);
        return Task.CompletedTask;
    }

    public Task BulkDeleteAsync(IEnumerable<TEntity> toDelete)
    {
        entities.RemoveRange(toDelete);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(TEntity entity)
    {
        if (entity is IAuditableEntity auditableEntity)
        {
            auditableEntity.OnModify();
        }

        entities.Update(entity);
        return Task.CompletedTask;
    }
}