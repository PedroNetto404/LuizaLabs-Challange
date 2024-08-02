using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using FavoriteProducts.UseCases.Features.Customers.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers;

public sealed class GetCustomerByIdEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<GetCustomerByIdEndpoint.HttpInputModel>
        .WithActionResult<CustomerViewModel>
{
    [HttpGet]
    [Route("/customers/{id:guid}")]
    [SwaggerOperation(
        Summary = "Get a customer by id",
        Description = "Fetch a customer with the given id",
        OperationId = "Customers_GetById",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(200, Type = typeof(CustomerViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<CustomerViewModel>> HandleAsync(
        HttpInputModel input,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new GetCustomerByIdQuery(input.Id), cancellationToken)
            .MatchAsync<CustomerDto, ActionResult<CustomerViewModel>>(
                (dto) => Ok(CustomerViewModel.FromDto(dto)),
                _ => BadRequest());

    [SwaggerSchema("The request model for the endpoint")]
    public sealed class HttpInputModel
    {
        [SwaggerParameter("The customer id"), FromRoute(Name = "id"), Required,
        DeniedValues("00000000-0000-0000-0000-000000000000")]
        public Guid Id { get; init; } = default!;
    }
}
