using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Delete;

public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .NotEqual(Guid.Empty);
    }
}
