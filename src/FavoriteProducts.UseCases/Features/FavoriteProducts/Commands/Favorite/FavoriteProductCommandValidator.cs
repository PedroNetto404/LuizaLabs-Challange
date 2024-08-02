using FluentValidation;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Favorite;

internal sealed class FavoriteProductCommandValidator : AbstractValidator<FavoriteProductCommand>
{
    public FavoriteProductCommandValidator()
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