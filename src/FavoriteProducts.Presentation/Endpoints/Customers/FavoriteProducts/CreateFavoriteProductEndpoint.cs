using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Commands.Favorite;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers.FavoriteProducts;

public sealed class CreateFavoriteProductEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<CreateFavoriteProductEndpoint.FavoriteProductInputModel>
        .WithActionResult<FavoriteProductViewModel>
{
    [HttpPost]
    [Route("/customers/{customerId:guid}/favorite-products")]
    [SwaggerOperation(
        Summary = "Create a favorite product",
        Description = "Create a new favorite product for a customer",
        OperationId = "Customers_CreateFavoriteProduct",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(201, Type = typeof(FavoriteProductViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<FavoriteProductViewModel>> HandleAsync(
        [FromBody] FavoriteProductInputModel input,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(
                new FavoriteProductCommand(
                    RouteData.Values["customerId"] is string customerId ? Guid.Parse(customerId) : Guid.Empty,
                    input.ProductId),
                cancellationToken)
            .MatchAsync<FavoriteProductDto, ActionResult<FavoriteProductViewModel>>(
                dto => Created($"/customers/{dto.CustomerId}/favorite-products/{dto.Id}", FavoriteProductViewModel.FromDto(dto)),
                _ => BadRequest());

    public sealed record FavoriteProductInputModel
    {
        [SwaggerParameter("The product id")]
        [Required]
        [DeniedValues("00000000-0000-0000-0000-000000000000")]
        public Guid ProductId { get; init; }
    }
}
