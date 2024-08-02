using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Update;

public sealed class UpdateCustomerCommandHandler(
    IRepository<Customer> repository,
    IUnitOfWork unitOfWork
) : ICommandHandler<UpdateCustomerCommand, CustomerDto>
{
    public async Task<Result<CustomerDto>> Handle(
        UpdateCustomerCommand request, 
        CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return DomainErrors.Customer.NotFound;
        }

        var maybeUpdateEmail = customer.UpdateEmail(request.Email);
        if (maybeUpdateEmail.IsFailure)
        {
            return maybeUpdateEmail.Error;
        }

        var maybeUpdateName = customer.UpdateName(request.Name);
        if (maybeUpdateName.IsFailure)
        {
            return maybeUpdateName.Error;
        }
        
        await repository.UpdateAsync(customer);
        return await unitOfWork.CommitAsync(cancellationToken) 
            ? CustomerDto.FromEntity(customer) 
            : DomainErrors.Customer.UpdateFailed;
    }
}