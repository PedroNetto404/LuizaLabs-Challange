using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers.FavoriteProducts;

public sealed class GetFavoriteProductByIdEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<GetFavoriteProductByIdEndpoint.GetFavoriteProductByIdModel>
        .WithActionResult<FavoriteProductViewModel>
{
    [HttpGet]
    [Route("/customers/{customerId:guid}/favorite-products/{favoriteProductId:guid}")]
    [SwaggerOperation(
        Summary = "Get a favorite product by id",
        Description = "Fetch a favorite product with the given id",
        OperationId = "Customers_GetFavoriteProductById",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(200, Type = typeof(FavoriteProductViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<FavoriteProductViewModel>> HandleAsync(
        GetFavoriteProductByIdModel request,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new GetFavoriteProductByIdQuery(request.CustomerId, request.FavoriteProductId), cancellationToken)
            .MatchAsync<FavoriteProductDto, ActionResult<FavoriteProductViewModel>>(
                (dto) => Ok(FavoriteProductViewModel.FromDto(dto)),
                _ => BadRequest());

    [SwaggerSchema("The request model for the endpoint")]
    public record GetFavoriteProductByIdModel
    {
        [FromRoute(Name = "customerId")]
        public Guid CustomerId { get; init; }

        [FromRoute(Name = "favoriteProductId")]
        public Guid FavoriteProductId { get; init; }
    }
}