namespace FavoriteProducts.Domain.Core.Results;
/// <summary>
/// Represents a wrapper for a value that may or may not be present. 
/// If the value is not present, an error is present.
/// </summary>
/// <typeparam name="TValue"></typeparam>
public sealed class Result<TValue> : Result
{
    private readonly TValue? _value;

    internal Result(TValue value) : base(null!) =>
        _value = value;

    internal Result(Error error) : base(error) =>
        _value = default!;

    public TValue Value =>
        IsFailure ? throw new InvalidOperationException("Cannot access value when error is present.") : _value!;

    public static implicit operator Result<TValue>(TValue value) =>
        Ok(value);

    public static implicit operator Result<TValue>(Error error) =>
        Fail<TValue>(error);

    public override bool IsOk => base.IsOk && _value is not null;

    public override bool IsFailure => base.IsFailure && _value is null;
}