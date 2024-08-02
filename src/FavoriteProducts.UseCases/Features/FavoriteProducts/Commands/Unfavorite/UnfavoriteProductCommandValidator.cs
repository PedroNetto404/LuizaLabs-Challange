using FluentValidation;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Unfavorite;

internal sealed class UnfavoriteProductCommandValidator : AbstractValidator<UnfavoriteProductCommand>
{
    public UnfavoriteProductCommandValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("CustomerId is required.");

        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("ProductId is required.");
    }
}