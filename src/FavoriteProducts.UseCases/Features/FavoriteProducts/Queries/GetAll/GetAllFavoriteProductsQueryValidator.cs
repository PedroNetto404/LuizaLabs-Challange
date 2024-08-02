using FluentValidation;

namespace FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetAll;

public sealed class GetAllFavoriteProductsQueryValidator : AbstractValidator<GetAllFavoriteProductsQuery>
{
    public GetAllFavoriteProductsQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0);
    }
}