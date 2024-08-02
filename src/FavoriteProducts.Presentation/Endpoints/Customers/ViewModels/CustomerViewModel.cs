using System.Text.Json.Serialization;
using FavoriteProducts.UseCases.Features.Customers.Dtos;

namespace FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;

public sealed record CustomerViewModel(
    Guid Id,
    string Name,
    string Email)
{
    public static CustomerViewModel FromDto(CustomerDto dto) =>
        new(
            dto.Id,
            dto.Name,
            dto.Email);
}
