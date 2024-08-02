using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Create;

public sealed class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MinimumLength(ProductTitle.MinLength)
            .MaximumLength(ProductTitle.MaxLength);

        RuleFor(x => x.Brand)
            .NotEmpty()
            .MinimumLength(ProductBrand.MinLength)
            .MaximumLength(ProductBrand.MaxLength);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MinimumLength(ProductDescription.MinLength)
            .MaximumLength(ProductDescription.MaxLength);

        RuleFor(x => x.Price)
            .GreaterThan(0);

        RuleFor(x => x.ImageUrl)
            .NotEmpty()
            .MaximumLength(500);
    }
}