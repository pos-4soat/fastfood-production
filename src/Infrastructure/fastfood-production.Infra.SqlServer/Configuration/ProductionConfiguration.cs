using fastfood_production.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace fastfood_production.Infra.SqlServer.Configuration;

[ExcludeFromCodeCoverage]
public class ProductionConfiguration : IEntityTypeConfiguration<ProductionEntity>
{
    public void Configure(EntityTypeBuilder<ProductionEntity> builder)
    {
        builder.ToTable("Production");
        builder.HasKey(c => c.Id).HasName("ProductionId");
        builder.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
        builder.Property(c => c.OrderId).HasColumnName("OrderId");
        builder.Property(c => c.Status).HasColumnName("Status");
        builder.Property(c => c.CreationDate).HasColumnName("CreationDate");
        builder.Property(c => c.UpdateDate).HasColumnName("UpdateDate");

        builder.HasMany(c => c.ProductionItems)
        .WithOne(u => u.Production)
        .HasForeignKey(y => y.ProductionId);
    }
}
