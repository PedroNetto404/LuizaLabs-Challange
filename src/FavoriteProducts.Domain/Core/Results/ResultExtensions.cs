namespace FavoriteProducts.Domain.Core.Results;

public static class ResultExtensions
{
    public static async Task<TOutput> MatchAsync<TInput, TOutput>(
        this Task<Result<TInput>> resultTask,
        Func<TInput, TOutput> onSuccess,
        Func<Error, TOutput> onError)

    {
        var result = await resultTask;
        return result.IsOk ? onSuccess(result.Value) : onError(result.Error);
    }

    public static async Task<TOutput> MatchAsync<TOutput>(
        this Task<Result> resultTask, 
        Func<TOutput> onSuccess, 
        Func<Error, TOutput> onError)
    {
        var result = await resultTask;
        return result.IsOk ? onSuccess() : onError(result.Error);
    }

    public static async Task<Result<TOutput>> MapAsync<TInput, TOutput>(
        this Task<Result<TInput>> resultTask,
        Func<TInput, TOutput> map)
    {
        var result = await resultTask;
        return result.IsOk ? map(result.Value) : result.Error;
    }
}