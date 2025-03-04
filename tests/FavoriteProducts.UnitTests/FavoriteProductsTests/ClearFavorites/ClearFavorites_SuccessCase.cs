using System;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.ClearFavorites;

public class ClearFavorites_SuccessCase(
    FavoriteProductsDomainServiceFixture favoriteProductsDomainServiceFixture,
    ProductBuilder productBuilder,
    CustomerBuilder customerBuilder,
    FavoriteProductBuilder favoriteProductBuilder
) :
    IClassFixture<FavoriteProductsDomainServiceFixture>,
    IClassFixture<FavoriteProductBuilder>,
    IClassFixture<ProductBuilder>,
    IClassFixture<CustomerBuilder>
{
    [Fact]
    public async Task ClearFavoritesAsync_ShouldReturnsdSuccess_WhenValidState()
    {
        // Arrange
        var customer = customerBuilder.Build();
        var product = productBuilder.Build();
        var favoriteProducts = Enumerable.Range(0, 5)
            .Select(_ => favoriteProductBuilder
                .WithCustomerId(customer.Id)
                .WithProductId(product.Id)
                .Build())
            .ToList();

        favoriteProductsDomainServiceFixture
            .CustomerRepositoryMock
            .Setup(x => x.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x => x.ListAsync(
                It.IsAny<FavoriteProductsByCustomerSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(favoriteProducts);
        
        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .ClearFavoritesAsync(customer.Id, It.IsAny<CancellationToken>());

        // Assert
        result.IsOk.Should().BeTrue();
    }
}