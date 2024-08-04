using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.UnfavoriteAsync;

public sealed class UnfavoriteAsync_ErrorCases(
    FavoriteProductBuilder favoriteProductBuilder,
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
                x.FirstOrDefaultAsync(
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

    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task UnfavoriteAsync_ShouldReturnFavoriteProductNotDeleted_WhenFavoriteProductNotDeleted()
    {
        //arrange
        var favoriteProduct = favoriteProductBuilder.Build();

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x =>
                x.FirstOrDefaultAsync(
                    It.IsAny<FavoriteProductByCustomerAndProductSpecification>(),
                    It.IsAny<CancellationToken>()))
            .ReturnsAsync(favoriteProduct);

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x => x.DeleteAsync(favoriteProduct, It.IsAny<CancellationToken>()));

        //act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .UnfavoriteAsync(favoriteProduct.CustomerId, favoriteProduct.ProductId);

        //assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FavoriteProduct.NotDeleted);
    }
}