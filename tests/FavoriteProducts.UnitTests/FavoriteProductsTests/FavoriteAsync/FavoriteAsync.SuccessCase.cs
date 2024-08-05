using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.FavoriteAsync;

public sealed class FavoriteAsync_SuccessCase(
    FavoriteProductsDomainServiceFixture favoriteProductsDomainServiceFixture,
    ProductBuilder productBuilder,
    CustomerBuilder customerBuilder,
    FavoriteProductBuilder favoriteProductBuilder
) : 
    IClassFixture<FavoriteProductsDomainServiceFixture>,
    IClassFixture<ProductBuilder>,
    IClassFixture<CustomerBuilder>,
    IClassFixture<FavoriteProductBuilder>
{

    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task FavoriteAsync_ShouldFavoriteProduct_WhenAllValidState()
    {
        // Arrange
        var customer = customerBuilder.Build();
        var product = productBuilder.WithStatus(true).Build();
        var favoriteProduct = 
            favoriteProductBuilder
                .WithProductId(product.Id)
                .WithCustomerId(customer.Id)
                .WithProductTitle(product.Title.Value)
                .Build();

        favoriteProductsDomainServiceFixture
            .CustomerRepositoryMock
            .Setup(x => x.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        favoriteProductsDomainServiceFixture
            .ProductRepositoryMock
            .Setup(x => x.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x => x.SingleOrDefaultAsync(It.IsAny<FavoriteProductByCustomerAndProductSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((FavoriteProduct?)null);

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock 
            .Setup(x => x.AddAsync(It.IsAny<FavoriteProduct>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(favoriteProduct);

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .FavoriteAsync(customer.Id, product.Id);

        // Assert
        result.IsOk.Should().BeTrue();
        result.Value.CustomerId.Should().Be(customer.Id);
        result.Value.ProductId.Should().Be(product.Id);
        result.Value.ProductTitle.Should().Be(product.Title.Value);
    }
}
