using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Core.Results;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;

namespace FavoriteProducts.Domain.Resources.Products;

public sealed class Product : Entity, IAuditableEntity
{
    private Product(
        ProductTitle title,
        ProductDescription description,
        ProductPrice price,
        ProductBrand brand,
        string imageUrl)
    {
        Title = title;
        Description = description;
        Price = price;
        Brand = brand;
        ImageUrl = imageUrl;
    }

    public static Result<Product> Create(
        ProductTitle title,
        ProductDescription description,
        ProductPrice price,
        ProductBrand brand,
        string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return DomainErrors.Product.InvalidProduct;
        }

        return new Product(title, description, price, brand, imageUrl)
        {
            DeletedAtUtc = null
        };
    }

    public ProductTitle Title { get; private set; }
    public ProductDescription Description { get; private set; }
    public ProductPrice Price { get; private set; }
    public ProductBrand Brand { get; private set; }
    public string ImageUrl { get; private set; }
    public bool Active { get; private set; }
    public DateTime CreatedAtUtc { get; } = DateTime.UtcNow;
    public DateTime ModifiedAtUtc { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAtUtc { get; set; } = DateTime.UtcNow;

    public void Activate() => Active = true;

    public void Deactivate() => Active = false;

    public Result UpdateImageUrl(string imageUrl) => ImageUrl == imageUrl ? Result.Ok() : ChangeImageUrl(imageUrl);

    public Result UpdatePrice(decimal price)
    {
        if (Price.Value == price)
        {
            return Result.Ok();
        }

        if (price <= 0)
        {
            return DomainErrors.Product.InvalidProductPrice;
        }

        Price = price;

        return Result.Ok();
    }

    public Result UpdateDescription(string description)
    {
        if(Description.Value == description)
        {
            return Result.Ok();
        }

        var maybe = ProductDescription.Create(description);
        if (maybe.IsFailure)
        {
            return maybe.Error;
        }

        Description = maybe.Value;

        return Result.Ok();
    }

    public Result UpdateBrand(string brand)
    {
        if(Brand.Value == brand)
        {
            return Result.Ok();
        }

        var maybe = ProductBrand.Create(brand);
        if (maybe.IsFailure)
        {
            return maybe.Error;
        }

        Brand = maybe.Value;

        return Result.Ok();
    }

    public Result UpdateTitle(string title)
    {   
        if(Title.Value == title)
        {
            return Result.Ok();
        }

        var maybe = ProductTitle.Create(title);
        if (maybe.IsFailure)
        {
            return maybe.Error;
        }

        Title = maybe.Value;

        return Result.Ok();
    }

    private Result ChangeImageUrl(string imageUrl)
    {
        if (string.IsNullOrEmpty(imageUrl))
        {
            return DomainErrors.Product.InvalidImageUrl;
        }

        ImageUrl = imageUrl;

        return Result.Ok();
    }

    //Required by EF Core
#pragma warning disable CS0628 // New protected member declared in sealed type
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    protected Product() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
#pragma warning restore CS0628 // New protected member declared in sealed type
}

