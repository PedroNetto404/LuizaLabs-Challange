using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;

namespace FavoriteProducts.Domain.Resources.Customers;

public sealed class Customer : Entity, IAuditableEntity
{
    public Customer(CustomerName name, Email email) =>
        (Name, Email) = (name, email);
    
    public CustomerName Name { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    public DateTime ModifiedAtUtc {get;  set;} = DateTime.UtcNow;
    public DateTime? DeletedAtUtc { get;  set; }
    
    public Result UpdateEmail(string email)
    {
        if (email == Email.Value)
        {
            return Result.Ok();
        }
        
        var maybeEmail = Email.Create(email);
        if (maybeEmail.IsFailure)
        {
            return maybeEmail.Error;
        }
        
        Email = maybeEmail.Value;
        
        return Result.Ok();
    }
    
    public Result UpdateName(string name)
    {
        var maybeName = CustomerName.Create(name);
        if (maybeName.IsFailure)
        {
            return maybeName.Error;
        }
        
        Name = maybeName.Value;
        
        return Result.Ok();
    }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS0628 // New protected member declared in sealed type
    protected Customer()
    {
    }
#pragma warning restore CS0628 // New protected member declared in sealed type
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
}

