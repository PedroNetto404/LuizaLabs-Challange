using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.Specifications;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Create;

internal sealed class CreateCustomerCommandHandler(
    IRepository<Customer> customerRepository
) : ICommandHandler<CreateCustomerCommand, CustomerDto>
{
    public async Task<Result<CustomerDto>> Handle(
        CreateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var maybeEmail = Email.Create(request.Email);
        if (maybeEmail is not { IsOk: true, Value: { } email })
        {
            return maybeEmail.Error;
        }

        var existingCustomer = await customerRepository.FirstOrDefaultAsync(
            new GetCustomerByEmailSpecification(email), 
            cancellationToken);
        if (existingCustomer is not null)
        {
            return DomainErrors.Customer.CustomerAlreadyExists;
        }

        var maybeCustomerName = CustomerName.Create(request.Name);
        if (maybeCustomerName is not { IsOk: true, Value: var customerName })
        {
            return maybeCustomerName.Error;
        }

        var customer = await customerRepository.AddAsync(new Customer(customerName, email), cancellationToken);

        return customer is not null
        ? CustomerDto.FromEntity(customer)
        : DomainErrors.Customer.CreateCustomerFailed;
    }
}