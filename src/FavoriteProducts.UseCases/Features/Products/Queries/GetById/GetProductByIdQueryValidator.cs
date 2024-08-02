using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Products.Queries.GetById;

public sealed class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(p => p.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}