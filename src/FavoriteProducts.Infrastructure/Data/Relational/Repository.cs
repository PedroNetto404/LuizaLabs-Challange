using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using FavoriteProducts.Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Infrastructure.Data.Relational;

internal sealed class Repository<TEntity>(FavoriteProductsContext context) :
    RepositoryBase<TEntity>(context),
    IRepository<TEntity>
    where TEntity : Entity;
