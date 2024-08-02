using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Customers.Queries.GetById;

public sealed class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdQueryValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty();
    }
}