using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.FavoriteAsync;

public sealed class FavoriteAsync_SuccessCase(
    FavoriteProductsDomainServiceFixture favoriteProductsDomainServiceFixture,
    ProductBuilder productBuilder,
    CustomerBuilder customerBuilder
) : 
    IClassFixture<FavoriteProductsDomainServiceFixture>,
    IClassFixture<ProductBuilder>,
    IClassFixture<CustomerBuilder>
{
    private readonly FavoriteProductsDomainServiceFixture _favoriteProductsDomainServiceFixture = favoriteProductsDomainServiceFixture;
    private readonly ProductBuilder _productBuilder = productBuilder;
    private readonly CustomerBuilder _customerBuilder = customerBuilder;

    [Fact]
    public async Task FavoriteAsync_ShouldFavoriteProduct_WhenAllValidState()
    {
        // Arrange
        var customer = _customerBuilder.Build();
        var product = _productBuilder.WithStatus(true).Build();

        _favoriteProductsDomainServiceFixture
            .CustomerRepositoryMock
            .Setup(x => x.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        _favoriteProductsDomainServiceFixture
            .ProductRepositoryMock
            .Setup(x => x.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        // Act
        var result = await _favoriteProductsDomainServiceFixture.FavoriteProductDomainService.FavoriteAsync(customer.Id, product.Id);

        // Assert
        result.IsOk.Should().BeTrue();
        result.Value.CustomerId.Should().Be(customer.Id);
        result.Value.ProductId.Should().Be(product.Id);
        result.Value.ProductTitle.Should().Be(product.Title.Value);
    }
}
