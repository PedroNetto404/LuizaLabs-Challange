using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FavoriteProducts.Presentation.Endpoints.Customers.ViewModels;
using FavoriteProducts.UseCases.Features.Customers.Commands.Create;
using FavoriteProducts.UseCases.Features.Customers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Customers;

public sealed class CreateCustomerEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<CreateCustomerEndpoint.CreateCustomerHttpInputModel>
        .WithActionResult<CustomerViewModel>
{
    [HttpPost]
    [Route("/customers")]
    [SwaggerOperation(
        Summary = "Create a customer",
        Description = "Create a new customer",
        OperationId = "Customers_Create",
        Tags = ["Customers"]
    )]
    [ProducesResponseType(201, Type = typeof(CustomerViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<CustomerViewModel>> HandleAsync(
        [FromBody] CreateCustomerHttpInputModel command,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new CreateCustomerCommand(command.Name, command.Email), cancellationToken)
            .MatchAsync<CustomerDto, ActionResult<CustomerViewModel>>(
                dto => Created($"/customers/{dto.Id}", CustomerViewModel.FromDto(dto)),
                _ => BadRequest());

    public sealed class CreateCustomerHttpInputModel
    {
        [SwaggerParameter("The customer name")]
        [StringLength(maximumLength: CustomerName.MaxLength, MinimumLength = CustomerName.MinLength)]
        [Required]
        public string Name { get; init; } = default!;

        [SwaggerParameter("The customer email")]
        [EmailAddress]
        [Required]
        public string Email { get; init; } = default!;
    }
}