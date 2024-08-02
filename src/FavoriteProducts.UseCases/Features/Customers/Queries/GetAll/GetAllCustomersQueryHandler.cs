using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.Specifications;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Queries.GetAll;

internal sealed class GetAllCustomersQueryHandler(
    IRepository<Customer> customerRepository
) : IQueryHandler<GetAllCustomersQuery, IEnumerable<CustomerDto>>
{
    public async Task<Result<IEnumerable<CustomerDto>>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await customerRepository.GetManyAsync(
            new GetAllCustomersSpecification(
                request.Page,
                request.PageSize,
                request.SortBy,
                request.SortOrder),
            cancellationToken);

        return Result.Ok(customers.Select(CustomerDto.FromEntity));
    }
}