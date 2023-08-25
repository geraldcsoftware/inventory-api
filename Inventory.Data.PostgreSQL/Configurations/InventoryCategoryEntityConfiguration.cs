using Inventory.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Data.PostgreSQL.Configurations;

public class InventoryCategoryEntityConfiguration : IEntityTypeConfiguration<InventoryCategory>
{
    public void Configure(EntityTypeBuilder<InventoryCategory> builder)
    {
        builder.ToTable("InventoryCategories");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
        builder.Property(x => x.Description).IsRequired(false).HasMaxLength(2000);
        builder.Property(x => x.Created).IsRequired().HasColumnType("timestamp with time zone");
        builder.Property(x => x.LastModified).IsRequired(false).HasColumnType("timestamp with time zone");
        builder.Property(x => x.Metadata).IsRequired(false).HasColumnType("jsonb");
    }
}