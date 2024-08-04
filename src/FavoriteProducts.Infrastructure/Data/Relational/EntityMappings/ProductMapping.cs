using FavoriteProducts.Domain.Resources.Products;
using FavoriteProducts.Domain.Resources.Products.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteProducts.Infrastructure.Data.Relational.EntityMappings;

public sealed class ProductMapping : AuditableEntityMapping<Product>
{
    public override void Configure(EntityTypeBuilder<Product> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("products");

        builder.Property(x => x.Title)
                .HasColumnName("title")
                .HasMaxLength(ProductTitle.MaxLength)
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => ProductTitle.Create(x).Value
                )
                .HasAnnotation("MaxLength", ProductTitle.MaxLength)
                .HasAnnotation("MinLength", ProductTitle.MinLength);
        
        builder.Property(p => p.ReviewScore)
                .HasColumnName("review_score")
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => ProductReviewScore.Create(x).Value
                )
                .HasAnnotation("MinValue", ProductReviewScore.MinValue);

        builder.Property(x => x.Description)
                .HasColumnName("description")
                .HasMaxLength(ProductDescription.MaxLength)
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => ProductDescription.Create(x).Value
                )
                .HasAnnotation("MaxLength", ProductDescription.MaxLength)
                .HasAnnotation("MinLength", ProductDescription.MinLength);

        builder.Property(x => x.Price)
                .HasColumnName("price")
                .HasColumnType("money")
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => ProductPrice.Create(x).Value
                )
                .HasAnnotation("MinValue", ProductPrice.MinValue)
                .HasPrecision(18, 2);

        builder
            .Property(x => x.Active)
            .HasColumnName("active")
            .IsRequired();

        builder.Property(x => x.ImageUrl)
                .HasColumnName("image_url")
                .HasMaxLength(500)
                .IsRequired()
                .HasAnnotation("MaxLength", 255)
                .HasAnnotation("MinLength", 5);

        builder.Property(x => x.Brand)
                .HasColumnName("brand")
                .HasMaxLength(ProductBrand.MaxLength)
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => ProductBrand.Create(x).Value
                )
                .HasAnnotation("MaxLength", ProductBrand.MaxLength)
                .HasAnnotation("MinLength", ProductBrand.MinLength);
    }
}