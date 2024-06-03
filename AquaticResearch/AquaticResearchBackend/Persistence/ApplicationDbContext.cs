using System.Diagnostics;
using Base.Tools;
using Core.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Mappings;

namespace Persistence;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext()
    {
        
    }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Species> Species { get; set; }
    public DbSet<Observation> Observations { get; set; }
    public DbSet<ResearchProject> ResearchProjects { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Species>().Map();
        modelBuilder.Entity<ResearchProject>().Map();
        modelBuilder.Entity<Observation>().Map();
        modelBuilder.Entity<Location>().Map();
        modelBuilder.Entity<Equipment>().Map();
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = ConfigurationHelper.GetConfiguration().Get("DefaultConnection", "ConnectionStrings");
            optionsBuilder.UseSqlServer(connectionString);
        }

        optionsBuilder.LogTo(message => Debug.WriteLine(message));
    }
}