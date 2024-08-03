using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Presentation.Endpoints.Products.ViewModels;
using FavoriteProducts.UseCases.Features.Products.Dtos;
using FavoriteProducts.UseCases.Features.Products.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Products;

public sealed class GetProductByIdEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<GetProductByIdEndpoint.GetProductByIdModel>
        .WithActionResult<ProductViewModel>
{
    [HttpGet]    
    [Route("/products/{productId:guid}")]
    [SwaggerOperation(
       Summary = "Get product by id",
       Description = "Fetch product by id",
       OperationId = "Products_GetById",
       Tags = ["Products"]
   )]
    [ProducesResponseType(200, Type = typeof(ProductViewModel))]
    [ProducesResponseType(404)]
    public override Task<ActionResult<ProductViewModel>> HandleAsync(
       GetProductByIdModel request,
       CancellationToken cancellationToken = default) =>
       sender
           .Send(new GetProductByIdQuery(request.ProductId), cancellationToken)
           .MatchAsync<ProductDto, ActionResult<ProductViewModel>>(
               (dto) => Ok(ProductViewModel.FromDto(dto)),
               _ => NotFound());
    
    [SwaggerSchema("The request model for the endpoint")]
    public sealed record GetProductByIdModel
    {
        [FromRoute(Name = "productId")]
        [Required]
        [SwaggerParameter("The product id")]
        [DeniedValues("00000000-0000-0000-0000-000000000000")]
        public Guid ProductId { get; init; }
    }
}