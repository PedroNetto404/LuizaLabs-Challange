using System;
using Bogus;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Infrastructure.Data.Relational;

public sealed class DatabaseSeed(FavoriteProductsContext context) : IDisposable
{
    public void Dispose()
    {
        // ReSharper disable once GCSuppressFinalizeForTypeWithoutDestructor
        GC.SuppressFinalize(this);
    }

    public async Task SeedAsync()
    {
        if (await context.Database.CanConnectAsync() is false) return;
        
        context.Customers.RemoveRange(context.Customers);
        context.Products.RemoveRange(context.Products);
        context.FavoriteProducts.RemoveRange(context.FavoriteProducts);

        var customers = GenerateCustomers();
        var products = GenerateProducts();

        await context.Customers.AddRangeAsync(customers);
        await context.Products.AddRangeAsync(products);

        foreach (var customer in customers)
        {
            var favoriteProducts = products
                .OrderBy(_ => Guid.NewGuid())
                .Take(10)
                .Select(p =>
                {
                    var favorite = new FavoriteProduct(customer.Id, p.Id, p.Title.Value)
                    {
                        DeletedAtUtc = null
                    };

                    return favorite;
                });


            await context.FavoriteProducts.AddRangeAsync(favoriteProducts);
        }

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

                return Product.Create(title, description, price, brand, reviewScore, imageUrl).Value;
            });

        return productFaker.Generate(10);
    }
}