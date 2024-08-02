using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.UseCases.Features.Products.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Products;

public sealed class DeleteProductEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<DeleteProductEndpoint.HttpInputModel>
        .WithActionResult
{
    [HttpDelete]
    [Route("/products/{productId:guid}")]
    [SwaggerOperation(
        Summary = "Delete product",
        Description = "Delete product by id",
        OperationId = "Products_Delete",
        Tags = ["Products"]
    )]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public override Task<ActionResult> HandleAsync(
        HttpInputModel request,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new DeleteProductCommand(request.ProductId), cancellationToken)
            .MatchAsync<ActionResult>(NoContent, BadRequest);
    
    [SwaggerSchema("The request model for the delete product endpoint")]
    public sealed record HttpInputModel
    {
        [FromRoute(Name = "productId")]
        [Required]
        [DeniedValues("00000000-0000-0000-0000-000000000000")]
        [SwaggerParameter("The product id")]
        public Guid ProductId { get; init; }
    }
}