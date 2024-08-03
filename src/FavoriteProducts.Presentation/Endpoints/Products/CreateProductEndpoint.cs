using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Ardalis.ApiEndpoints;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using FavoriteProducts.Presentation.Endpoints.Products.ViewModels;
using FavoriteProducts.UseCases.Features.Products.Commands.Create;
using FavoriteProducts.UseCases.Features.Products.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FavoriteProducts.Presentation.Endpoints.Products;

public sealed class CreateProductEndpoint(ISender sender) :
    EndpointBaseAsync
    .WithRequest<CreateProductEndpoint.CreateProductModel>
    .WithActionResult<ProductViewModel>
{
    [HttpPost]
    [Route("/products")]
    [SwaggerOperation(
        Summary = "Create a product",
        Description = "Create a product",
        OperationId = "Products_Create",
        Tags = ["Products"]
    )]
    [ProducesResponseType(201, Type = typeof(ProductViewModel))]
    [ProducesResponseType(400)]
    public override Task<ActionResult<ProductViewModel>> HandleAsync(
        [FromBody]CreateProductModel request,
        CancellationToken cancellationToken = default) =>
        sender
            .Send(new CreateProductCommand(
                request.Title,
                request.Brand,
                request.Description,
                request.Price,
                request.ReviewScore,
                request.ImageUrl), cancellationToken).MatchAsync<ProductDto, ActionResult<ProductViewModel>>(
                dto => Created($"products/{dto.Id}", ProductViewModel.FromDto(dto)),
                (_) => BadRequest());

    [SwaggerSchema("The request model for the create product endpoint")]
    public sealed class CreateProductModel
    {
        [SwaggerParameter("The product title"), FromBody, Required,
        StringLength(maximumLength: ProductTitle.MaxLength, MinimumLength = ProductTitle.MinLength)]
        [Display(Name = "Product Title")]
        [SwaggerRequestBody("The product title", Required = true)]
        public string Title { get; init; } = string.Empty;

        [SwaggerParameter("The product brand"), FromBody, Required,
        StringLength(maximumLength: ProductBrand.MaxLength, MinimumLength = ProductBrand.MinLength)]
        [Display(Name = "Product Brand")]
        [SwaggerRequestBody("The product brand", Required = true)]
        public string Brand { get; init; } = string.Empty;

        [SwaggerParameter("The product description"), FromBody, Required,
        StringLength(maximumLength: ProductDescription.MaxLength, MinimumLength = ProductDescription.MinLength)]
        [Display(Name = "Product Description")]
        [SwaggerRequestBody("The product description", Required = true)]
        public string Description { get; init; } = string.Empty;

        [SwaggerParameter("The product price"), FromBody, Required,
        Range((double)ProductPrice.MinValue, double.MaxValue)]
        [Display(Name = "Product Price")]
        [SwaggerRequestBody("The product price", Required = true)]
        public decimal Price { get; init; } = ProductPrice.MinValue;
        
        [SwaggerParameter("The product review score"), FromBody, Required,
        Range(ProductReviewScore.MinValue, int.MaxValue)]
        [Display(Name = "Product Review Score")]
        [SwaggerRequestBody("The product review score", Required = true)]
        public int ReviewScore { get; init; } = ProductReviewScore.MinValue;

        [SwaggerParameter("The product image url"), FromBody, Required]
        [Display(Name = "Product Image URL")]
        [SwaggerRequestBody("The product image url", Required = true)]
        public string ImageUrl { get; init; } = string.Empty;
    }
}   
