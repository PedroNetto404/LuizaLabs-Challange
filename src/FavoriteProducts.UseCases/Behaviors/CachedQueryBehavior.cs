using System.Collections;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.UseCases.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteProducts.UseCases.Behaviors;

internal sealed class CachedQueryBehavior<TQuery, TResponse>(
    ICacheProvider cacheProvider,
    ILogger<CachedQueryBehavior<TQuery, TResponse>> logger
)
    : IPipelineBehavior<TQuery, TResponse>
    where TQuery : ICachedQuery<TResponse>
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TQuery request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var cachedValue = await cacheProvider.GetAsync<TResponse>(
            request.CacheKey,
            cancellationToken
        );
        if (cachedValue is not null)
        {
            logger.LogInformation("Cache hit for key {CacheKey}", request.CacheKey);
            return cachedValue;
        }

        logger.LogInformation("Cache miss for key {CacheKey}", request.CacheKey);
        var result = await next();
        if (result.IsOk)
        {
            await cacheProvider.SetAsync(
                request.CacheKey,
                result,
                request.Expiration,
                cancellationToken
            );
        }

        return result;
    }
}