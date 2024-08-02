using System.Text.Json.Serialization;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;

public sealed record CustomerViewModel
{
    public Guid Id { get; init; }

    public string Name { get; init; } = string.Empty; 

    public string Email { get; init; } = string.Empty;

    public static CustomerViewModel FromDto(CustomerDto dto) =>
        new()
        {
            Id = dto.Id,
            Name = dto.Name,
            Email = dto.Email
        };
}
