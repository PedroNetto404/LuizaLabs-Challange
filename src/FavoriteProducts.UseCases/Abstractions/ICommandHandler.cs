using FavoriteProducts.Domain.Core.Results;
using MediatR;

namespace FavoriteProducts.UseCases.Abstractions;

/// <summary>
/// Markup interface to represent a Command that returns nothing.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
public interface ICommandHandler<in TCommand> :
    IRequestHandler<TCommand, Result>
    where TCommand : ICommand;

/// <summary>
/// Markup interface to represent a Command Handler that returns a response.
/// </summary>
/// <typeparam name="TCommand"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public interface ICommandHandler<in TCommand, TResponse> :
    IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;