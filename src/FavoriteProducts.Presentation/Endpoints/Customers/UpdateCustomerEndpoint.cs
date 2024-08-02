using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Commands.Update;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers;

public sealed class UpdateCustomerEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<UpdateCustomerEndpoint.HttpInputModel>
        .WithActionResult<CustomerViewModel>
{
    [HttpPut]
    [Route("/customers/{customerId:guid}")]
    [SwaggerOperation(
        Summary = "Update a customer",
        Description = "Update a customer with the given id",
        OperationId = "Customers_Update",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(200, Type = typeof(CustomerViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<CustomerViewModel>> HandleAsync(
        [FromBody]HttpInputModel model,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new UpdateCustomerCommand(
                RouteData.Values["customerId"] is string customerId ? Guid.Parse(customerId) : Guid.Empty, 
                model.Name, 
                model.Email), cancellationToken)
            .MatchAsync<CustomerDto, ActionResult<CustomerViewModel>>(
                dto => Ok(CustomerViewModel.FromDto(dto)),
                _ => BadRequest());

    public sealed class HttpInputModel
    {
        [SwaggerParameter("The customer name"),
        Required,
        StringLength(maximumLength: CustomerName.MaxLength, MinimumLength = CustomerName.MinLength)]
        public string Name { get; init; } = default!;

        [SwaggerParameter("The customer email"),
        Required,
        EmailAddress]
        public string Email { get; init; } = default!;
    }
}
