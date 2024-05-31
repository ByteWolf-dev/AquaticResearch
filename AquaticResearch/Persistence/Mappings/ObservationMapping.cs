using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings;

public static class ObservationMapping
{
    public static void Map(this EntityTypeBuilder<Observation> entity)
    {
        entity.ToTable("Observations");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Notes)
            .HasMaxLength(1000);

        entity.Property(e => e.ObservationDateTime)
            .IsRequired();

        entity
            .HasOne(e => e.ResearchProject)
            .WithMany()
            .HasForeignKey("ProjectId");

        entity
            .HasMany(e => e.Equipment)
            .WithMany();

        entity
            .HasOne(e => e.Location)
            .WithMany()
            .HasForeignKey("LocationId");
    }
}