using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Delete;

public sealed class DeleteCustomerCommandValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
