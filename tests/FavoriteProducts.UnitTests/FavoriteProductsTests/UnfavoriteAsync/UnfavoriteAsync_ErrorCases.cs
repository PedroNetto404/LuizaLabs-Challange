using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.UnfavoriteAsync;

public sealed class UnfavoriteAsync_ErrorCases(
    FavoriteProductsDomainServiceFixture favoriteProductsDomainServiceFixture) :
    IClassFixture<FavoriteProductsDomainServiceFixture>,
    IClassFixture<FavoriteProductBuilder>
{
    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task UnfavoriteAsync_ShouldReturnFavoriteProductNotFound_WhenFavoriteProductNotExists()
    {
        //arrange
        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x =>
                x.SingleOrDefaultAsync(
                    It.IsAny<FavoriteProductByCustomerAndProductSpecification>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync((FavoriteProduct?)null);

        //act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .UnfavoriteAsync(It.IsAny<Guid>(), It.IsAny<Guid>());

        //assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FavoriteProduct.NotFound);
    }
}