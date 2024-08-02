using Bogus;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;

namespace FavoriteProducts.UnitTests.Builders;

public sealed class ProductBuilder
{
    private static readonly Faker _faker = new();
    private ProductTitle _title = ProductTitle.Create(_faker.Commerce.ProductName()).Value;
    private ProductDescription _description = ProductDescription.Create(_faker.Commerce.ProductDescription()).Value;
    private ProductPrice _price = ProductPrice.Create(_faker.Finance.Amount()).Value;
    private ProductBrand _brand = ProductBrand.Create(_faker.Company.CompanyName()).Value;
    private string _imageUrl = _faker.Image.PicsumUrl();
    private ProductReviewScore _reviewScore = ProductReviewScore.Create(_faker.Random.Int(1, 5)).Value;
    private bool _active = true;

    public ProductBuilder WithTitle(ProductTitle title)
    {
        _title = title;
        return this;
    }

    public ProductBuilder WithDescription(ProductDescription description)
    {
        _description = description;
        return this;
    }

    public ProductBuilder WithPrice(ProductPrice price)
    {
        _price = price;
        return this;
    }

    public ProductBuilder WithBrand(ProductBrand brand)
    {
        _brand = brand;
        return this;
    }

    public ProductBuilder WithImageUrl(string imageUrl)
    {
        _imageUrl = imageUrl;
        return this;
    }

    public Product Build()
    {
        var product = Product.Create(_title, _description, _price, _brand, _reviewScore, _imageUrl).Value;

        if (_active)
        {
            product.Activate();
        }
        else 
        {
            product.Deactivate();
        }

        return product;
    }

    public ProductBuilder WithStatus(bool status)
    {
        _active = status;
        return this;
    }
}
