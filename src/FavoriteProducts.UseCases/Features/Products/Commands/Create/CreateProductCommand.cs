using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Create;

public sealed record CreateProductCommand(
    string Title,
    string Brand,
    string Description,
    decimal Price,
    int ReviewScore,
    string ImageUrl
) : ICommand<ProductDto>;