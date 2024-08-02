using System.Text.RegularExpressions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;

namespace FavoriteProducts.Domain.Resources.Customers.ValueObjects;

public sealed partial record Email
{
    public static readonly Regex Regex = MyRegex();
    public const string RegexPattern = "^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$";
    
    public string Value { get; }
    
    private Email(string value) => Value = value;

    public static Result<Email> Create(string email)
    {
        if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email))
        {
            return DomainErrors.Customer.InvalidEmail;
        }
        
        return new Email(email);
    }

    [GeneratedRegex(RegexPattern, RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}