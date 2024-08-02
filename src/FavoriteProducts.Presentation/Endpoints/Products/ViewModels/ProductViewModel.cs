using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.Presentation.Endpoints.Products.ViewModels;

public sealed record ProductViewModel
{
    [JsonPropertyName("id")]
    public Guid Id { get; init; }

    [JsonPropertyName("title")]
    [Required]
    [StringLength(maximumLength: ProductTitle.MaxLength, MinimumLength = ProductTitle.MinLength, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("description")]
    [Required]
    [StringLength(maximumLength: ProductDescription.MaxLength, MinimumLength = ProductDescription.MinLength, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]
    public string Description { get; init; } = string.Empty;

    [JsonPropertyName("price")]
    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Price { get; init; }

    [JsonPropertyName("brand")]
    [Required]
    [StringLength(maximumLength: ProductBrand.MaxLength, MinimumLength = ProductBrand.MinLength, ErrorMessage = "The {0} field must be between {2} and {1} characters.")]
    public string Brand { get; init; } = string.Empty;

    [JsonPropertyName("image_url")]
    public string ImageUrl { get; init; } = string.Empty;

    [JsonPropertyName("active")]
    public bool Active { get; init; }

    [JsonPropertyName("created_at_utc")]
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