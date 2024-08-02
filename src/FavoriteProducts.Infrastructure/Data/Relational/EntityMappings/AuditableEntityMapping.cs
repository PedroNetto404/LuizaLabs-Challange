using FavoriteProducts.Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteProducts.Infrastructure.Data.Relational.EntityMappings;

public abstract class AuditableEntityMapping<TAuditableEntity> : EntityMapping<TAuditableEntity>
    where TAuditableEntity : class, IAuditableEntity
{
    public override void Configure(EntityTypeBuilder<TAuditableEntity> builder)
    {
        base.Configure(builder);
        
        builder.Property(e => e.CreatedAtUtc).HasColumnName("created_at_utc").HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(e => e.ModifiedAtUtc).HasColumnName("modified_at_utc").HasColumnType("timestamp with time zone").IsRequired();
        builder.Property(e => e.DeletedAtUtc).HasColumnName("deleted_at_utc").HasColumnType("timestamp with time zone");

        builder.HasQueryFilter(x => !x.DeletedAtUtc.HasValue);
    }
}