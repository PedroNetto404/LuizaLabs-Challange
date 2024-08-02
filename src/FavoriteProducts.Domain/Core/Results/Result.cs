
namespace FavoriteProducts.Domain.Core.Results;

public class Result
{
    private readonly Error? _error;

    protected Result(Error error) =>
        _error = error;

    public static Result Ok() => new(default!);

    public static Result<TValue> Ok<TValue>(TValue value) => new(value);

    public static Result Fail(Error error) => new(error);

    public static Result<TValue> Fail<TValue>(Error error) => new(error);

    public bool IsOk => _error is null;

    public bool IsFailure => _error is not null;

    public Error Error =>
        IsOk ? throw new InvalidOperationException("Cannot access error when value is present.") : _error!;

    public static implicit operator Result(Error error) =>
        Fail(error);
}