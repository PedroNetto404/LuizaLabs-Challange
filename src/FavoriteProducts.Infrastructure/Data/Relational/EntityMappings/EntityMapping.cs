using FavoriteProducts.Domain.Core.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FavoriteProducts.Infrastructure.Data.Relational.EntityMappings;

public abstract class EntityMapping<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class, IEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Id).HasColumnName("id").HasColumnType("uuid").IsRequired();
    }
}