using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.UseCases.Abstractions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FavoriteProducts.UseCases.Behaviors;

internal sealed class LoggingBehavior<TCommand, TResponse>(
    ILogger<TCommand> logger
)
    : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommandBase
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TCommand request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var commandName = request.GetType().Name;

        try
        {
            logger.LogInformation("Handling command {CommandName}", commandName);

            var result = await next();
            if (result.IsOk)
            {
                logger.LogInformation("Command {CommandName} handled successfully", commandName);
            }
            else
            {
                logger.LogWarning("Command {CommandName} failed with error {Error}", commandName, result.Error);
            }

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error handling command {CommandName}", commandName);
            throw;
        }
    }
}