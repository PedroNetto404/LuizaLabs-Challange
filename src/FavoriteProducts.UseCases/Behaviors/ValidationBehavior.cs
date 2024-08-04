using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Exceptions;
using FluentValidation;
using MediatR;
using ValidationException = FavoriteProducts.UseCases.Exceptions.ValidationException;

namespace FavoriteProducts.UseCases.Behaviors;

internal sealed class ValidationBehavior<TCommand, TResponse>(
    IEnumerable<IValidator<TCommand>> validators
)
    : IPipelineBehavior<TCommand, TResponse>
    where TCommand : ICommandBase
    where TResponse : Result
{
    public async Task<TResponse> Handle(
        TCommand request,
        RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
        {
            return await next();
        }

        var context = new ValidationContext<TCommand>(request);
        var validationErrors = await Task.WhenAll(
            validators.Select(v => v.ValidateAsync(context, cancellationToken))
        );

        var failures = validationErrors
            .SelectMany(result => result.Errors)
            .ToList();

        if (failures.Count != 0)
        {
            throw new ValidationException(
                failures.Select(f => new ValidationError(f.PropertyName, f.ErrorMessage))
            );
        }

        return await next();
    }
}