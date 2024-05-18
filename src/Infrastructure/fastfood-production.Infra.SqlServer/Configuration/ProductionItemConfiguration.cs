using fastfood_production.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace fastfood_production.Infra.SqlServer.Configuration
{
    [ExcludeFromCodeCoverage]
    public class ProductionItemConfiguration : IEntityTypeConfiguration<ProductionItemEntity>
    {
        public void Configure(EntityTypeBuilder<ProductionItemEntity> builder)
        {
            builder.ToTable("ProductionItem");
            builder.HasKey(c => c.Id).HasName("ProductionItemId");
            builder.Property(c => c.Id).HasColumnName("Id").ValueGeneratedOnAdd();
            builder.Property(c => c.ProductionId).HasColumnName("ProductionId");
            builder.Property(c => c.Quantity).HasColumnName("Quantity");
            builder.Property(c => c.Name).HasColumnName("Name");

            builder.HasOne(c => c.Production)
            .WithMany(u => u.ProductionItems)
            .HasForeignKey(u => u.ProductionId);
        }
    }
}