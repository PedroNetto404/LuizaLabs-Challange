using System.Text.Json.Serialization;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;

namespace FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;

public sealed record FavoriteProductViewModel
{
    [JsonPropertyName("product_id")]
    public Guid ProductId { get; init; }

    [JsonPropertyName("customer_id")]
    public Guid CustomerId { get; init; }

    [JsonPropertyName("created_at_utc")]
    public DateTime CreatedAtUtc { get; init; }

    public static FavoriteProductViewModel FromDto(FavoriteProductDto dto) =>
        new()
        {
            ProductId = dto.ProductId,
            CustomerId = dto.CustomerId,
            CreatedAtUtc = dto.CreatedAtUtc
        };
}
