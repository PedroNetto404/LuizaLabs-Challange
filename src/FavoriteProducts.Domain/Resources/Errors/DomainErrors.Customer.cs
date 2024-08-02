using FavoriteProducts.Domain.Core.Results;
using CustomerEntity = FavoriteProducts.Domain.Resources.Customers.Customer;

namespace FavoriteProducts.Domain.Resources.Errors;

public static partial class DomainErrors
{
    public static class Customer
    {
        public static readonly DomainError<CustomerEntity> NotFound = new("customer_not_found", "Customer not found.");
        public static readonly DomainError<CustomerEntity> InvalidCustomerName = new("invalid_customer_name", "Customer can't have invalid name.");
        public static readonly DomainError<CustomerEntity> InvalidEmail = new("invalid_email", "Customer can't have invalid email.");
        public static readonly DomainError<CustomerEntity> CreateCustomerFailed = new("create_customer_failed", "Failed to create customer.");
        public static readonly DomainError<CustomerEntity> DeleteCustomerFailed = new("delete_customer_failed", "Failed to delete customer.");
        public static readonly DomainError<CustomerEntity> CustomerAlreadyExists = new("customer_already_exists", "Customer already exists.");
        public static readonly DomainError<CustomerEntity> UpdateFailed = new("update_failed", "Failed to update customer.");
    }
}