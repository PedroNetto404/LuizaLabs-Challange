using FavoriteProducts.UseCases.Abstractions;
using FavoriteProducts.UseCases.Features.Products.Dtos;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Update;

public sealed record UpdateProductCommand(
    Guid ProductId,
    string Title,
    string Brand,
    string Description,
    decimal Price,
    string ImageUrl,
    bool Active
) : ICommand<ProductDto>;
