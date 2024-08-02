using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Unfavorite;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers.FavoriteProducts;

public sealed class UnfavoriteProductEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<UnfavoriteProductEndpoint.HttpInputModel>
        .WithoutResult
{
    [HttpDelete]
    [Route("/customers/{customerId:guid}/favorite-products/{favoriteProductId:guid}")]
    [SwaggerOperation(
        Summary = "Unfavorite a product",
        Description = "Remove a favorite product from a customer",
        OperationId = "Customers_UnfavoriteProduct",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public override Task<ActionResult> HandleAsync(
        HttpInputModel request,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(
                new UnfavoriteProductCommand(
                    request.CustomerId, 
                    request.FavoriteProductId), 
                cancellationToken)
            .MatchAsync<ActionResult>(NoContent, BadRequest);

    [SwaggerSchema("The request model for the unfavorite product endpoint")]
    public record HttpInputModel
    {
        [FromRoute(Name = "customerId")]
        [SwaggerParameter("The customer id")]
        public Guid CustomerId { get; init; }

        [FromRoute(Name = "favoriteProductId")]
        [SwaggerParameter("The favorite product id")]
        public Guid FavoriteProductId { get; init; }
    }
}
