using System;
using System.Linq.Expressions;
using Ardalis.Specification;
using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Shared.Enums;

namespace FavoriteProducts.Domain.Extensions;

public static class ISpecificationBuilderExtensions
{
    public static ISpecificationBuilder<T> Paginate<T>(this ISpecificationBuilder<T> query, int page, int pageSize) =>
        query
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

    public static IOrderedSpecificationBuilder<TEntity> SortBy<TEntity>(
        this ISpecificationBuilder<TEntity> Query,
        string sortBy, 
        SortOrder sortOrder, 
        IDictionary<string, Expression<Func<TEntity, object?>>> sortingExpresions) where TEntity : IEntity
        {
            IOrderedSpecificationBuilder<TEntity>? orderedQuery = null;

            if(!string.IsNullOrWhiteSpace(sortBy) && sortingExpresions.Keys.Contains(sortBy))
            {
                var sortExpression = sortingExpresions[sortBy]!;

                if(sortOrder == SortOrder.Asc)
                {
                    orderedQuery = Query.OrderBy(sortExpression);
                }
                else
                {
                    orderedQuery = Query.OrderByDescending(sortExpression);
                }
            }

            return orderedQuery ?? Query.OrderBy(p => p.Id);
        }

}
