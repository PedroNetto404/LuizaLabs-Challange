using FluentValidation;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.ClearFavorites;

internal sealed class ClearFavoritesValidator : AbstractValidator<ClearFavoritesCommand>
{
    public ClearFavoritesValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty)
            .WithMessage("CustomerId is required.");
    }
}
