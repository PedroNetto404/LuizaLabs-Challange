namespace FavoriteProducts.Domain.Core.Results;

public record Error(string Code, string Message)
{
    private static readonly Lazy<Error> _unexpectedError =
        new(() => new Error("unexpected_error", "An unexpected error occurred."));

    public static Error Unexpected => _unexpectedError.Value;

    public static Error MultipleErrors(IEnumerable<Error> errors) =>
        new("multiple_errors", $"Multiple errors occurred: {string.Join(", ", errors.Select(e => e.Message))}");
}