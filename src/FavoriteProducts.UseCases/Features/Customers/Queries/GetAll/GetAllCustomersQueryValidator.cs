using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FluentValidation;

namespace FavoriteProducts.UseCases.Features.Customers.Queries.GetAll;

public sealed class GetAllCustomersQueryValidator : AbstractValidator<GetAllCustomersQuery>
{
    public GetAllCustomersQueryValidator()
    {
        RuleFor(x => x.SortBy)
            .NotEmpty()
            .Equals(nameof(CustomerDto.Name))
            .Equals(nameof(CustomerDto.Email));

        RuleFor(x => x.SortOrder)
            .IsInEnum();

        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
} 