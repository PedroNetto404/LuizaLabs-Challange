using System.ComponentModel.DataAnnotations;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.UseCases.Features.Customers.Commands.Delete;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers;

public sealed class DeleteCustomerEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<DeleteCustomerEndpoint.DeleteCustomerModel>
        .WithActionResult
{
    [HttpDelete]
    [Route("/customers/{id:guid}")]
    [SwaggerOperation(
        Summary = "Delete a customer by id",
        Description = "Delete a customer with the given id",
        OperationId = "Customers_Delete",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public override Task<ActionResult> HandleAsync(
        DeleteCustomerModel input,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new DeleteCustomerCommand(input.Id), cancellationToken)
            .MatchAsync<ActionResult>(
                NoContent,
                _ => BadRequest());

    [SwaggerSchema("The request model for the delete customer endpoint")]
    public sealed class DeleteCustomerModel
    {
        [SwaggerParameter("The customer id"), FromRoute(Name = "id"), Required,
        DeniedValues("00000000-0000-0000-0000-000000000000")]
        public Guid Id { get; init; } = default!;
    }
}
