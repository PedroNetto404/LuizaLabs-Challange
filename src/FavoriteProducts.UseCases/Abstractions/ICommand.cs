using FavoriteProducts.Domain.Core.Results;
using MediatR;

namespace FavoriteProducts.UseCases.Abstractions;

/// <summary>
/// Markup interface to represent a Command that returns nothing.
/// </summary>

public interface ICommand :
    IRequest<Result>,
    ICommandBase;

/// <summary>
/// Markup interface to represent a Command that returns a response.
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommand<TResponse> :
    IRequest<Result<TResponse>>,
    ICommandBase;
