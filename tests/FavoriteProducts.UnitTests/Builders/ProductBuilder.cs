using FavoriteProducts.Domain;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;

namespace FavoriteProducts.UnitTests.Builders;

public sealed class ProductBuilder
{
    private ProductTitle _title = ProductTitle.Create("Smartphone").Value;
    private ProductDescription _description = ProductDescription.Create("Anything").Value;
    private ProductPrice _price = ProductPrice.Create(1000.00m).Value;
    private ProductBrand _brand = ProductBrand.Create("Samsung").Value;
    private string _imageUrl = "http://image.com";
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
        var product = Product.Create(_title, _description, _price, _brand, _imageUrl).Value;
        product.GetType()!.GetProperty("Active")!.SetValue(product, _active);

        return product;
    }


    public ProductBuilder WithInactiveStatus()
    {
        _active = false;
        return this;
    }
}
