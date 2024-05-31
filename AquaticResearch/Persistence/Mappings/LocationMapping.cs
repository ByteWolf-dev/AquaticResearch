using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings;

public static class LocationMapping
{
    public static void Map(this EntityTypeBuilder<Location> entity)
    {
        entity.ToTable("Locations");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(200);

        entity
            .HasIndex(e => e.Name)
            .IsUnique();

        entity.Property(e => e.Latitude)
            .IsRequired();

        entity.Property(e => e.Longitude)
            .IsRequired();
    }
}