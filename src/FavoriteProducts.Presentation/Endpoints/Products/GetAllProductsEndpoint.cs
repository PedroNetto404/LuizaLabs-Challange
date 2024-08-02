using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Shared.Enums;
using FavoriteProducts.Presentation.Endpoints.Products.ViewModels;
using FavoriteProducts.UseCases.Features.Products.Dtos;
using FavoriteProducts.UseCases.Features.Products.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SortOrderEnum = FavoriteProducts.Domain.Shared.Enums.SortOrder;

namespace FavoriteProducts.Presentation.Endpoints.Products;

public sealed class GetAllProductsEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<GetAllProductsEndpoint.HttpInputModel>
        .WithActionResult<IEnumerable<ProductViewModel>>
{
    [HttpGet]
    [Route("/products")]
    [SwaggerOperation(
        Summary = "Get all products",
        Description = "Fetch all products",
        OperationId = "Products_GetAll",
        Tags = ["Products"]
    )]
    [ProducesResponseType(200, Type = typeof(IEnumerable<ProductViewModel>))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<IEnumerable<ProductViewModel>>> HandleAsync(
        HttpInputModel request,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new GetAllProductsQuery(
                request.SortBy,
                Enum.Parse<SortOrderEnum>(request.SortOrder),
                request.Active,
                request.Page,
                request.PageSize), cancellationToken)
            .MatchAsync<IEnumerable<ProductDto>, ActionResult<IEnumerable<ProductViewModel>>>(
                (dtos) => Ok(dtos.Select(ProductViewModel.FromDto)),
                _ => BadRequest());
    
    [SwaggerSchema("The request model for the endpoint")]
    public sealed record HttpInputModel
    {
        [FromQuery(Name = "page")]
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        [SwaggerParameter("The page number")]
        public int Page { get; init; } = 1;

        [FromQuery(Name = "page_size")]
        [SwaggerParameter("The page size")]
        [DefaultValue(10)]
        [Range(1, 100)]
        public int PageSize { get; init; } = 10;

        [FromQuery(Name = "sort-by")]
        [SwaggerParameter("The field to sort by")]
        [DefaultValue(nameof(ProductViewModel.Id))]
        [AllowedValues(
            nameof(ProductViewModel.Id),
            nameof(ProductViewModel.Title), 
            nameof(ProductViewModel.Price), 
            nameof(ProductViewModel.Brand))]
        public string SortBy { get; init; } = nameof(ProductDto.Title);

        [FromQuery(Name = "sort-order")]
        [DefaultValue(nameof(SortOrderEnum.Asc))]
        [SwaggerParameter("The sort order")]
        [AllowedValues(nameof(SortOrderEnum.Asc), nameof(SortOrderEnum.Desc))]
        public string SortOrder { get; init; } = nameof(SortOrderEnum.Asc);

        [FromQuery(Name = "active")]
        [DefaultValue(true)]
        [SwaggerParameter("The active status")]
        public bool Active { get; init; } = true;
    }
}
