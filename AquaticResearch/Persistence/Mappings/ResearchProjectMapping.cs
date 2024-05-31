using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Mappings;

public static class ResearchProjectMapping
{
    public static void Map(this EntityTypeBuilder<ResearchProject> entity)
    {
        entity.ToTable("ResearchProjects");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Title)
            .IsRequired()
            .HasMaxLength(200);
        
        entity.HasIndex(e => e.Title)
            .IsUnique();

        entity.HasOne(e => e.Species)
            .WithMany()
            .HasForeignKey("SpeciesId");
    }
}