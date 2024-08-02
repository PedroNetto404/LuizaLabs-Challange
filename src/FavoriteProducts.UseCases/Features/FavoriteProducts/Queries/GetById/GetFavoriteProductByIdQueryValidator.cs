using FluentValidation;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetById;

public sealed class GetFavoriteProductByIdQueryValidator : AbstractValidator<GetFavoriteProductByIdQuery>
{
    public GetFavoriteProductByIdQueryValidator()
    {
        RuleFor(x => x.FavoriteProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);

        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
