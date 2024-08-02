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
    IRepository<Customer> customerRepository,
    IUnitOfWork unitOfWork
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

        var existingCustomer = await customerRepository.GetOneAsync(
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

        var customer = new Customer(customerName, email);
        await customerRepository.AddAsync(customer);

        return await unitOfWork.CommitAsync(cancellationToken)
        ? CustomerDto.FromEntity(customer)
        : DomainErrors.Customer.CreateCustomerFailed;
    }
}