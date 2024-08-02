using System.Text.Json;
using FavoriteProducts.UseCases.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace FavoriteProducts.Infrastructure.Cache;

internal sealed class MemoryCacheProvider(
    IMemoryCache memoryCache) : ICacheProvider
{
    public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        if (!memoryCache.TryGetValue(key, out T? value))
        {
            return Task.FromResult<T?>(default);
        }

        return Task.FromResult(value);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        memoryCache.Remove(key);
        return Task.CompletedTask;
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default)
    {
        var options = new MemoryCacheEntryOptions();
        if (expiration.HasValue)
        {
            options.SetAbsoluteExpiration(expiration.Value);
        }

        memoryCache.Set(key, value, options);
        return Task.CompletedTask;
    }
}