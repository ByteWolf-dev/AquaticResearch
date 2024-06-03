using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings;

public static class SpeciesMapping
{
    public static void Map(this EntityTypeBuilder<Species> entity)
    {
        entity.ToTable("Species");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Name)
            .IsRequired()
            .HasMaxLength(100);

        entity
            .HasIndex(e => e.ScientificName)
            .IsUnique();

        entity.Property(e => e.ScientificName)
            .IsRequired()
            .HasMaxLength(200);
    }
}