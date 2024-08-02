using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Ardalis.Specification;
using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Shared.Enums;

namespace FavoriteProducts.Domain.Extensions;

public static class ISpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> Paginate<T>(this ISpecificationBuilder<T> query, int page, int pageSize)
    {
        if (page <= 0 || pageSize <= 0)
        {
            throw new ArgumentException("Page and page size must be greater than zero.");
        }

        return query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);
    }

    public static IOrderedSpecificationBuilder<TEntity> SortBy<TEntity>(
        this ISpecificationBuilder<TEntity> query,
        string sortBy, 
        SortOrder sortOrder, 
        IDictionary<string, Expression<Func<TEntity, object>>> sortingMap) where TEntity : IEntity =>
        string.IsNullOrWhiteSpace(sortBy) ||
        !sortingMap.TryGetValue(sortBy, out var sortExpression)
            ? query.OrderBy(x => x.Id)
            : sortOrder == SortOrder.Asc
                ? query.OrderBy(sortExpression!)
                : query.OrderByDescending(sortExpression!);
}