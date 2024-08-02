using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Queries.GetById;

public sealed class GetCustomerByIdQueryHandler(IRepository<Customer> customerRepository) : IQueryHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<Result<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer  = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return DomainErrors.Customer.NotFound;
        }

        return CustomerDto.FromEntity(customer);
    }
}