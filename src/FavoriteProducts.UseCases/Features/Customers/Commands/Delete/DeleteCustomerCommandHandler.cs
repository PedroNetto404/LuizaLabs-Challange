using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.Customers.Commands.Delete;

public sealed class DeleteCustomerCommandHandler(
    IRepository<Customer> customerRepository, 
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteCustomerCommand>
{
    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await customerRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
        {
            return DomainErrors.Customer.NotFound;
        }

        await customerRepository.DeleteAsync(customer);
        return await unitOfWork.CommitAsync(cancellationToken) is true
            ? Result.Ok()
            : DomainErrors.Customer.DeleteCustomerFailed;
    }
}
