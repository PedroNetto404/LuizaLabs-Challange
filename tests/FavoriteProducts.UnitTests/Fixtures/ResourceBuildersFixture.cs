using FavoriteProducts.Domain;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.UnitTests.Builders;

namespace FavoriteProducts.UnitTests.Fixtures;

public sealed class ResourceBuildersFixture
{
    public CustomerBuilder CustomerBuilder { get; }
    public ProductBuilder ProductBuilder { get; }

    public ResourceBuildersFixture()
    {
        CustomerBuilder = new();
        ProductBuilder = new();
    } 
}
