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
    .WithRequest<UpdateProductEndpoint.UpdateProductModel>
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
        [FromBody]UpdateProductModel httpInput,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new UpdateProductCommand(
                RouteData.Values["productId"] is string productId ? Guid.Parse(productId) : Guid.Empty,
                httpInput.Title,
                httpInput.Brand,
                httpInput.Description,
                httpInput.Price,
                httpInput.ReviewScore,
                httpInput.ImageUrl,
                httpInput.Active), cancellationToken)
            .MatchAsync<ProductDto, ActionResult<ProductViewModel>>(
                (dto) => Ok(ProductViewModel.FromDto(dto)),
                _ => BadRequest());

    [SwaggerSchema("The request model for the update product endpoint")]
    public sealed record UpdateProductModel
    {
        [Required]
        [SwaggerParameter("The product title")]
        [StringLength(maximumLength: ProductTitle.MaxLength, MinimumLength = ProductTitle.MinLength)]
        public string Title { get; init; } = default!;

        [Required]
        [SwaggerParameter("The product brand")]
        [StringLength(maximumLength: ProductBrand.MaxLength, MinimumLength = ProductBrand.MinLength)]
        public string Brand { get; init; } = default!;

        [Required]
        [SwaggerParameter("The product description")]
        [StringLength(maximumLength: ProductDescription.MaxLength, MinimumLength = ProductDescription.MinLength)]
        public string Description { get; init; } = default!;

        [Required]
        [SwaggerParameter("The product price")]
        [Range((double)ProductPrice.MinValue, double.MaxValue)]
        public decimal Price { get; init; }

        [Required]
        [SwaggerParameter("The product review score")]
        [Range(ProductReviewScore.MinValue, int.MaxValue)]
        public int ReviewScore { get; set; }

        [Required]
        [SwaggerParameter("The product image url")]
        public string ImageUrl { get; init; } = default!;

        [Required]
        [SwaggerParameter("The product active status")]
        [DefaultValue(true)]
        public bool Active { get; init; }
    }
}