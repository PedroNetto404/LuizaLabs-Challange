using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Update;

public sealed class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(p => p.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(p => p.Name)
            .NotEmpty()
            .NotNull()
            .MinimumLength(CustomerName.MinLength)
            .MaximumLength(CustomerName.MaxLength);

        RuleFor(p => p.Email)
            .EmailAddress()
            .NotEmpty()
            .NotNull();
    }
}