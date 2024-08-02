using FavoriteProducts.UseCases.Abstractions;

namespace FavoriteProducts.UseCases.Features.Products.Commands.Delete;

public sealed record DeleteProductCommand(Guid ProductId) : ICommand;
