using FavoriteProducts.Domain.Core.Results;
using MediatR;

namespace FavoriteProducts.UseCases.Abstractions;

/// <summary>
/// Markup interface to represent a query that returns a response.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IQuery<TResponse> : IRequest<Result<TResponse>>;

