using System.Text.Json.Serialization;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;

public sealed record FavoriteProductViewModel(
    Guid Id,
    Guid ProductId,
    Guid CustomerId,
    string ProductTitle,
    DateTime CreatedAtUtc)
{
    public static FavoriteProductViewModel FromDto(FavoriteProductDto dto) =>
        new(
            dto.Id,
            dto.ProductId,
            dto.CustomerId,
            dto.ProductTitle,
            dto.CreatedAtUtc);
}
