using FavoriteProducts.Domain.Resources.Customers;
using FavoriteProducts.Domain.Resources.Customers.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteProducts.Infrastructure.Data.Relational.EntityMappings;

public sealed class CustomerMapping : AuditableEntityMapping<Customer>
{
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
        base.Configure(builder);
        
        builder.ToTable("customers");

        builder.Property(x => x.Name)
            .HasColumnName("name")
            .HasMaxLength(CustomerName.MaxLength)
            .IsRequired()
            .HasConversion(
                x => x.Value,
                x => CustomerName.Create(x).Value
            )
            .HasAnnotation("MaxLength", CustomerName.MaxLength)
            .HasAnnotation("MinLength", CustomerName.MinLength);

        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasConversion(
                x => x.Value,
                x => Email.Create(x).Value
            );
    }
}