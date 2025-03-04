using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Dtos;
using FavoriteProducts.UseCases.Features.FavoriteProducts.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers.FavoriteProducts;

public sealed class GetAllFavoriteProductsEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<GetAllFavoriteProductsEndpoint.GetAllFavoriteProductsModel>
        .WithActionResult<IEnumerable<FavoriteProductViewModel>>

{
    [HttpGet]
    [Route("/customers/{customerId:guid}/favorite-products")]
    [SwaggerOperation(
        Summary = "Get all favorite products",
        Description = "Fetch all favorite products for a customer",
        OperationId = "Customers_FavoriteProducts_GetAll",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(200, Type = typeof(IEnumerable<FavoriteProductViewModel>))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<IEnumerable<FavoriteProductViewModel>>> HandleAsync(
        GetAllFavoriteProductsModel request,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new GetAllFavoriteProductsQuery(
                RouteData.Values["customerId"] is string customerId ? Guid.Parse(customerId) : Guid.Empty,
                request.Page,
                request.PageSize), cancellationToken)
            .MatchAsync
            <
                IEnumerable<FavoriteProductDto>, 
                ActionResult<IEnumerable<FavoriteProductViewModel>>
            >(
                dtos => Ok(dtos.Select(FavoriteProductViewModel.FromDto)),
                _ => BadRequest()
            );

    [SwaggerSchema("The request model for the endpoint")]
     public sealed record GetAllFavoriteProductsModel
    {
        [Range(1, int.MaxValue)]
        [FromQuery(Name = "page")]
        [SwaggerParameter("The page number")]
        public int Page { get; init; } = 1;

        [Range(1, 100)]
        [FromQuery(Name = "page-size")]
        [SwaggerParameter("The page size")]
        public int PageSize { get; init; } = 10;
    }
}