using Ardalis.ApiEndpoints;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.ClearFavorites;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers.FavoriteProducts;

public sealed class ClearFavoriteProductsEndpoint(ISender sender)
    : EndpointBaseAsync
        .WithoutRequest
        .WithoutResult
{
    [HttpDelete]
    [Route("/customers/{customerId:guid}/favorite-products")]
    [SwaggerOperation(
        Summary = "Clear favorite products",
        Description = "Remove all favorite products from a customer",
        OperationId = "Customers_ClearFavoriteProducts",
        Tags = ["Customers"]
    )]
    public override Task HandleAsync(CancellationToken cancellationToken = default) =>
        sender.Send(new ClearFavoritesCommand(
            RouteData.Values["customerId"] is string customerId ? Guid.Parse(customerId) : Guid.Empty
        ), cancellationToken);
}