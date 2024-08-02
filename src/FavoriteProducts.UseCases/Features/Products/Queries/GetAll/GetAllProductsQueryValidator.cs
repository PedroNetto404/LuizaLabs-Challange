using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Products.Queries.GetAll;

public sealed class GetAllProductsQueryValidator : AbstractValidator<GetAllProductsQuery>
{
    public GetAllProductsQueryValidator()
       {
        RuleFor(x => x.SortOrder).IsInEnum();
        RuleFor(x => x.Page).GreaterThan(0);
        RuleFor(x => x.PageSize).GreaterThan(0).LessThanOrEqualTo(100);
    }
}
