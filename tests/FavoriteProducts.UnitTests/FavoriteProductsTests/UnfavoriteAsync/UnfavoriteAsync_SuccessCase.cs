using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.UnfavoriteAsync;

public class UnfavoriteAsync_SuccessCase(
    FavoriteProductBuilder favoriteProductBuilder,
    FavoriteProductsDomainServiceFixture favoriteProductsDomainServiceFixture) : 
    IClassFixture<FavoriteProductsDomainServiceFixture>,
    IClassFixture<FavoriteProductBuilder>
{
    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task UnfavoriteAsync_ShouldUnfavoriteProduct_WhenValidState()
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
        
        //act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .UnfavoriteAsync(favoriteProduct.CustomerId, favoriteProduct.ProductId);

        //assert
        result.IsOk.Should().BeTrue();
    }
}
