using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.FavoriteProducts;
using FavoriteProducts.Domain.Resources.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteProducts.Infrastructure.Data.Relational.EntityMappings;

public sealed class FavoriteProductMapping : AuditableEntityMapping<FavoriteProduct>
{
    public override void Configure(EntityTypeBuilder<FavoriteProduct> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("favorite_products");

        builder.Property(x => x.CustomerId)
            .HasColumnName("customer_id")
            .IsRequired()
            .HasColumnType("uuid");

        builder.HasOne<Customer>()
         .WithMany()
         .HasForeignKey(x => x.CustomerId);

        builder.Property(x => x.ProductId)
            .HasColumnName("product_id")
            .IsRequired()
            .HasColumnType("uuid");

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(x => x.ProductId);

        builder.HasIndex(x => new { x.CustomerId, x.ProductId })
            .IsUnique()
            .HasFilter("deleted_at_utc IS NULL");
    }
}