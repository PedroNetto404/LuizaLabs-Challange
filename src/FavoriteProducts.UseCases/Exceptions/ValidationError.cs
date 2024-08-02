namespace FavoriteProducts.UseCases.Exceptions;

public sealed record ValidationError(string Property, string Message);