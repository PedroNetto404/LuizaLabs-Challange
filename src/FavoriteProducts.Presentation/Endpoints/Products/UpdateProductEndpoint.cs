using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FavoriteProducts.Presentation.Endpoints.Products.ViewModels;
using FavoriteProducts.UseCases.Features.Products.Commands.Update;
using FavoriteProducts.UseCases.Features.Products.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Products;

public sealed class UpdateProductEndpoint(ISender sender) :
    EndpointBaseAsync
        .WithRequest<UpdateProductEndpoint.HttpInputModel>
        .WithActionResult<ProductViewModel>
{   
    [HttpPut]
    [Route("/products/{productId:guid}")]
    [SwaggerOperation(
        Summary = "Update a product",
        Description = "Update a product",
        OperationId = "Products_Update",
        Tags = ["Products"]
    )] 
    [ProducesResponseType(200, Type = typeof(ProductViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<ProductViewModel>> HandleAsync(
        HttpInputModel httpInput,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new UpdateProductCommand(
                httpInput.ProductId,
                httpInput.Model.Title,
                httpInput.Model.Brand,
                httpInput.Model.Description,
                httpInput.Model.Price,
                httpInput.Model.ImageUrl,
                httpInput.Model.Active), cancellationToken)
            .MatchAsync<ProductDto, ActionResult<ProductViewModel>>(
                (dto) => Ok(ProductViewModel.FromDto(dto)),
                _ => BadRequest());

    [SwaggerSchema("The request model for the update product endpoint")]
    public sealed record HttpInputModel
    {
        [SwaggerParameter("The product id")]
        [DeniedValues("00000000-0000-0000-0000-000000000000")]
        [FromRoute(Name = "productId")]
        [Required]
        public Guid ProductId { get; init; }

        [FromBody]
        [Required]
        [SwaggerParameter("The product model")]
        public Product Model { get; init; } = default!;

        public record Product
        {
            [Required]
            [SwaggerParameter("The product title")]
            [JsonPropertyName("title")]
            [StringLength(maximumLength: ProductTitle.MaxLength, MinimumLength = ProductTitle.MinLength)]
            public string Title { get; init; } = default!;
            
            [Required]
            [SwaggerParameter("The product brand")]
            [JsonPropertyName("brand")]
            [StringLength(maximumLength: ProductBrand.MaxLength, MinimumLength = ProductBrand.MinLength)]
            public string Brand { get; init; } = default!;

            [Required]
            [SwaggerParameter("The product description")]
            [JsonPropertyName("description")]
            [StringLength(maximumLength: ProductDescription.MaxLength, MinimumLength = ProductDescription.MinLength)]
            public string Description { get; init; } = default!;

            [Required]
            [SwaggerParameter("The product price")]
            [Range((double)ProductPrice.MinValue, double.MaxValue)]
            [JsonPropertyName("price")]
            public decimal Price { get; init; }

            [Required]
            [SwaggerParameter("The product image url")]
            [JsonPropertyName("image_url")]
            public string ImageUrl { get; init; } = default!;

            [Required]
            [SwaggerParameter("The product active status")]
            [DefaultValue(true)]
            [JsonPropertyName("active")]
            public bool Active { get; init; }
        }
    }
}