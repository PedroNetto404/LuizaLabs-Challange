using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Errors;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.FavoriteProducts.Specifications;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.UnitTests.Builders;
using FavoriteProducts.UnitTests.Fixtures;
using FluentAssertions;
using Moq;

namespace FavoriteProducts.UnitTests.FavoriteProductsTests.FavoriteAsync;

public sealed class FavoriteAsync_ErrorCase(
    FavoriteProductsDomainServiceFixture favoriteProductsDomainServiceFixture,
    ProductBuilder productBuilder,
    CustomerBuilder customerBuilder
) :
    IClassFixture<FavoriteProductsDomainServiceFixture>,
    IClassFixture<FavoriteProductBuilder>,
    IClassFixture<ProductBuilder>,
    IClassFixture<CustomerBuilder>
{
    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task FavoriteAsync_ShouldReturnsFailure_WhenProductAlreadyFavorited()
    {
        // Arrange
        var customer = customerBuilder.Build();
        var product = productBuilder.WithStatus(true).Build();

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
            .ReturnsAsync(new FavoriteProduct(customer.Id, product.Id, product.Title.Value));

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .FavoriteAsync(customer.Id, product.Id);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FavoriteProduct.AlreadyFavorite);
    }

    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task FavoriteAsync_ShouldReturnProductNotFound_WhenProductNotExists()
    {
        // Arrange
        var customer = customerBuilder.Build();

        favoriteProductsDomainServiceFixture
            .CustomerRepositoryMock
            .Setup(x => x.GetByIdAsync(customer.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(customer);

        favoriteProductsDomainServiceFixture
            .ProductRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Product?)null);

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x => x.SingleOrDefaultAsync(It.IsAny<FavoriteProductByCustomerAndProductSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((FavoriteProduct?)null);

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .FavoriteAsync(customer.Id, It.IsAny<Guid>());

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Product.NotFound);
    }

    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task FavoriteAsync_ShouldReturnCustomerNotFound_WhenCustomerNotExists()
    {
        // Arrange
        var product = productBuilder.WithStatus(true).Build();

        favoriteProductsDomainServiceFixture
            .CustomerRepositoryMock
            .Setup(x => x.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Customer?)null);

        favoriteProductsDomainServiceFixture
            .ProductRepositoryMock
            .Setup(x => x.GetByIdAsync(product.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(product);

        favoriteProductsDomainServiceFixture
            .FavoriteProductsRepositoryMock
            .Setup(x => x.SingleOrDefaultAsync(It.IsAny<FavoriteProductByCustomerAndProductSpecification>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((FavoriteProduct?)null);

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .FavoriteAsync(It.IsAny<Guid>(), product.Id);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.Customer.NotFound);
    }

    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task FavoriteAsync_ShouldReturnsError_WhenProductIsInactive()
    {
        // Arrange
        var customer = customerBuilder.Build();
        var product = productBuilder.WithStatus(false).Build();

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

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .FavoriteAsync(customer.Id, product.Id);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FavoriteProduct.CannotFavoriteInactiveProduct);
    }

    [Fact, Trait("Category", "Unit"), Trait("Resource", "FavoriteProducts")]
    public async Task FavoriteAsync_ShouldReturnsError_WhenCommitFailed()
    {
        // Arrange
        var customer = customerBuilder.Build();
        var product = productBuilder.WithStatus(true).Build();

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

        // Act
        var result = await favoriteProductsDomainServiceFixture
            .FavoriteProductDomainService
            .FavoriteAsync(customer.Id, product.Id);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(DomainErrors.FavoriteProduct.NotSaved);
    }
}