using Ardalis.Specification;

namespace FavoriteProducts.Domain.Core.Abstractions;

public interface IRepository<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity;
