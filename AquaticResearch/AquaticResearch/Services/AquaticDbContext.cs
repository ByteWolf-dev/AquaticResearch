using AquaticResearch.Model;
using AquaticResearch.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace AquaticResearch.Services;

public class AquaticDbContext : DbContext
{
    public AquaticDbContext(DbContextOptions<AquaticDbContext> options)
        : base(options)
    {
    }

    public DbSet<Equipment> Equipments { get; set; }
    /*public DbSet<Location> Locations { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Observation> Observations { get; set; }
    public DbSet<ResearchProject> ResearchProjects { get; set; } */
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipment>()
            .HasKey(e => e.Id);
        
        modelBuilder.Entity<Equipment>()
            .Property(e => e.Id)
            .ValueGeneratedOnAdd();
        
        
    }
}