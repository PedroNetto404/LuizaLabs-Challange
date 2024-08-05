using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.ClearFavorites;

public class ClearFavorites_ErrorCases(
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
    public async Task ClearFavoritesAsync_ShouldReturnCustomerNotFound_WhenCustomerNotExists()
    {
        // Arrange
        var customer = customerBuilder.Build();

        favoriteProductsDomainServiceFixture
            .CustomerRepositoryMock
            .Setup(x => x.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .ClearFavoritesAsync(customer.Id, It.IsAny<CancellationToken>());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Customer.NotFound);
    }
}