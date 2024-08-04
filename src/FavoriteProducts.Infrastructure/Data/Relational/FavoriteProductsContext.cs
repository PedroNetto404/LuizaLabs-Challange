using FavoriteProducts.Domain.Core.Abstractions;
using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.Products;
using Microsoft.EntityFrameworkCore;

namespace FavoriteProducts.Infrastructure.Data.Relational;

public sealed class FavoriteProductsContext(
    DbContextOptions<FavoriteProductsContext> options
) : DbContext(options)
{
    public DbSet<FavoriteProduct> FavoriteProducts { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Customer> Customers { get; set; }
    
    public async Task<bool> CommitAsync(CancellationToken cancellationToken = default) => 
        await SaveChangesAsync(cancellationToken) > 0;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(FavoriteProductsContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
