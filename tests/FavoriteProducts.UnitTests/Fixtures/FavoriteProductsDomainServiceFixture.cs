using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Resources.AcrossAggregateServices;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.Products;
using Moq;

namespace FavoriteProducts.UnitTests.Fixtures;

public class FavoriteProductsDomainServiceFixture
{
    public Mock<IRepository<Customer>> CustomerRepositoryMock { get; set; }
    public Mock<IRepository<Product>> ProductRepositoryMock { get; set; }
    public Mock<IRepository<FavoriteProduct>> FavoriteProductsRepositoryMock { get; set; }
    public FavoriteProductDomainService FavoriteProductDomainService { get; set; }

    public FavoriteProductsDomainServiceFixture()
    {
        FavoriteProductsRepositoryMock = new Mock<IRepository<FavoriteProduct>>();
        ProductRepositoryMock = new Mock<IRepository<Product>>();
        CustomerRepositoryMock = new Mock<IRepository<Customer>>();
        FavoriteProductDomainService = new FavoriteProductDomainService(
            FavoriteProductsRepositoryMock.Object,
            ProductRepositoryMock.Object,
            CustomerRepositoryMock.Object);
    }
}