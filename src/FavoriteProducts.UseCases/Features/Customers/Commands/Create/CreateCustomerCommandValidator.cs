using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Create;

internal sealed class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The name is required.")
            .MinimumLength(CustomerName.MinLength).WithMessage($"The name must be at least {CustomerName.MinLength} characters long.")
            .MaximumLength(CustomerName.MaxLength).WithMessage($"The name must not exceed {CustomerName.MaxLength} characters.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("The email is required.")
            .EmailAddress().WithMessage("The email is invalid.");
    }
}
