using Bogus;
using FavoriteProducts.Domain.Resources.FavoriteProducts;

namespace FavoriteProducts.UnitTests.Builders;

public sealed class FavoriteProductBuilder
{
    private static readonly Faker _faker = new();
    private Guid _customerId = _faker.Random.Guid();
    private Guid _productId = _faker.Random.Guid();
    private string _productTitle = _faker.Commerce.ProductName();

    public FavoriteProductBuilder WithCustomerId(Guid customerId)
    {
        _customerId = customerId;
        return this;
    }

    public FavoriteProductBuilder WithProductId(Guid productId)
    {
        _productId = productId;
        return this;
    }

    public FavoriteProduct Build()
    {
        var favoriteProduct = new FavoriteProduct(
            _customerId, 
            _productId, 
            _productTitle);

        return favoriteProduct;
    }

    public FavoriteProductBuilder WithProductTitle(string value)
    {
        _productTitle = value;
        return this;
    }
}