using System;
using Bogus;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Infrastructure.Data.Relational;

public sealed class DatabaseSeeder(FavoriteProductsContext context)
{
    public async Task SeedAsync()
    {
        var customers = GenerateCustomers();
        var products = GenerateProducts();

        await context.Customers.AddRangeAsync(customers);
        await context.Products.AddRangeAsync(products);

        await context.FavoriteProducts.AddRangeAsync(
            customers.SelectMany(customer =>
                products.OrderBy(_ => Guid.NewGuid())
                        .Take(10)
                        .ToList()
                        .Select(product => new FavoriteProduct(
                            customer.Id,
                            product.Id,
                            product.Title.Value))));

        await context.SaveChangesAsync();
    }

    private static List<Customer> GenerateCustomers()
    {
        var customerFaker = new Faker<Customer>()
            .CustomInstantiator(f =>
            {
                var name = CustomerName.Create(f.Name.FullName()).Value;
                var email = Email.Create(f.Internet.Email()).Value;
                return new Customer(name, email)
                {
                    DeletedAtUtc = null
                };
            });

        return customerFaker.Generate(10);
    }

    private static List<Product> GenerateProducts()
    {
        var productFaker = new Faker<Product>()
            .CustomInstantiator(f =>
            {
                var title = ProductTitle.Create(f.Commerce.ProductName()).Value;
                var description = ProductDescription.Create(f.Commerce.ProductDescription()).Value;
                var price = ProductPrice.Create(f.Random.Decimal(1, 1000)).Value;
                var brand = ProductBrand.Create(f.Company.CompanyName()).Value;
                var reviewScore = ProductReviewScore.Create(f.Random.Int(1, 5)).Value;
                var imageUrl = f.Internet.Url();

                var product = Product.Create(title, description, price, brand, reviewScore, imageUrl).Value;
                product.Activate();

                return product;
            });

        return productFaker.Generate(10);
    }
}