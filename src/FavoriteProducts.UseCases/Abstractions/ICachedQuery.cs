namespace FavoriteProducts.UseCases.Abstractions;

public interface ICachedQuery<TResponse> :
    IQuery<TResponse>
{
    string CacheKey { get; }

    TimeSpan? Expiration { get; }
}

