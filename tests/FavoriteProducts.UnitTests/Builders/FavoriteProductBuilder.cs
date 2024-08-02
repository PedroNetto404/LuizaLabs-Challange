using FavoriteProducts.Domain.Resources.FavoriteProducts;

namespace FavoriteProducts.UnitTests.Builders;

public sealed class FavoriteProductBuilder
{
    private Guid _customerId = Guid.NewGuid();
    private Guid _productId = Guid.NewGuid();
    private bool _isDeleted = false;

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

    public FavoriteProductBuilder WithDeletedStatus()
    {
        _isDeleted = true;
        return this;
    }

    public FavoriteProduct Build()
    {
        var favoriteProduct = new FavoriteProduct(_customerId, _productId);
        return favoriteProduct;
    }
}