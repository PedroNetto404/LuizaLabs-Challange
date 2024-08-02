using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.Presentation.Endpoints.Products.ViewModels;

public sealed record ProductViewModel
{
    public Guid Id { get; init; }
    public string Title { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public string Brand { get; init; } = string.Empty;
    public string ImageUrl { get; init; } = string.Empty;
    public bool Active { get; init; }
    public DateTime? CreatedAtUtc { get; init; }

    public static ProductViewModel FromDto(ProductDto dto) =>
        new()
        {
            Id = dto.Id,
            Title = dto.Title,
            Brand = dto.Brand,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            Active = dto.Active,
            CreatedAtUtc = dto.CreatedAtUtc
        };
}