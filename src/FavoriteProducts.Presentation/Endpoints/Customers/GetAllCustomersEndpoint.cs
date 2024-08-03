using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.Customers.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using SortOrderEnum = FavoriteProducts.Domain.Shared.Enums.SortOrder;

namespace FavoriteProducts.Presentation.Endpoints.Customers;

public sealed class GetAllCustomersEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<GetAllCustomersEndpoint.GetAllCustomersModel>
        .WithActionResult<IEnumerable<CustomerViewModel>>
{
    [HttpGet]
    [Route("/customers")]
    [SwaggerOperation(
        Summary = "Get all customers",
        Description = "Fetch all customers",
        OperationId = "Customers_GetAll",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerViewModel>))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<IEnumerable<CustomerViewModel>>> HandleAsync(
        GetAllCustomersModel query,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new GetAllCustomersQuery(
                query.SortBy,
                Enum.Parse<SortOrderEnum>(query.SortOrder),
                query.Page,
                query.PageSize), cancellationToken)
            .MatchAsync<IEnumerable<CustomerDto>, ActionResult<IEnumerable<CustomerViewModel>>>(
                (dtos) => Ok(dtos.Select(CustomerViewModel.FromDto)),
                _ => BadRequest());

    [SwaggerSchema("The request model for the endpoint")]
    public sealed record GetAllCustomersModel
    {
        [FromQuery(Name = "page")]
        [DefaultValue(1)]
        [Range(1, int.MaxValue)]
        public int Page { get; init; }

        [FromQuery(Name = "page_size")]
        [DefaultValue(10)]
        [Range(1, 100)]
        public int PageSize { get; init; }

        [FromQuery(Name = "sort_by")]
        [DefaultValue(nameof(CustomerViewModel.Name))]
        [AllowedValues(
            nameof(CustomerViewModel.Name), 
            nameof(CustomerViewModel.Email), 
            nameof(CustomerViewModel.Id))]
        public string SortBy { get; init; } = nameof(CustomerViewModel.Id);

        [FromQuery(Name = "sort_order")]
        [DefaultValue(nameof(SortOrderEnum.Asc))]
        [AllowedValues(nameof(SortOrderEnum.Asc), nameof(SortOrderEnum.Desc))]
        public string SortOrder { get; init; } = nameof(SortOrderEnum.Asc);
    }
}