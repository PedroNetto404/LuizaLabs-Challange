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
    [Fact]
    public async Task UnfavoriteAsync_ShouldUnfavoriteProduct_WhenValidState()
    {
        //arrange
        var favoriteProduct = favoriteProductBuilder.Build();

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x => 
                x.GetOneAsync(
                    It.IsAny<FavoriteProductByCustomerAndProductSpecification>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(favoriteProduct);
        
        favoriteProductsDomainServiceFixture
            .UnitOfWorkMock
            .Setup(x => x.CommitAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        //act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .UnfavoriteAsync(favoriteProduct.CustomerId, favoriteProduct.ProductId);

        //assert
        result.IsOk.Should().BeTrue();
    }
}
