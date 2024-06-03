using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings;

public static class EquipmentMapping
{
    public static void Map(this EntityTypeBuilder<Equipment> entity)
    {
        entity.ToTable("Equipment");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity
            .HasIndex(e => e.Name)
            .IsUnique();

        entity.Property(e => e.Description)
            .HasMaxLength(500);
    }
}