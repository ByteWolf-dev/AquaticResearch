// See https://aka.ms/new-console-template for more information

using Base.Core;
using Base.Tools;
using Core.Contracts;
using Core.Contracts.Repositories;
using Core.Entities;
using ImportConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using Persistence.UnitOfWork;
using Persistence.UnitOfWork.Repositories;

ConfigureDependencyInjector();
await RecreateDatabaseAsync();
await Import();

void ConfigureDependencyInjector()
{
    var configuration = ConfigurationHelper.GetConfiguration();
    var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

    AppService.ServiceCollection = new ServiceCollection();
    AppService.ServiceCollection
        .AddSingleton(configuration)
        .AddDbContext<DbContext, ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString))
        .AddScoped<IUnitOfWork, UnitOfWork>()
        .AddTransient<IEquipmentRepository, EquipmentRepository>()
        .AddTransient<ILocationRepository, LocationRepository>()
        .AddTransient<IObservationRepository, ObservationRepository>()
        .AddTransient<IResearchProjectRepository, ResearchProjectRepository>()
        .AddTransient<ISpeciesRepository, SpeciesRepository>()
        .AddTransient<IImportService, ImportService>();
    
    AppService.BuildServiceProvider();
}

async Task RecreateDatabaseAsync()
{
    Console.WriteLine("=====================");
    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        Console.WriteLine("Deleting database ...");
        await uow.DeleteDatabaseAsync();
        
//        Console.WriteLine("Creating and migrating database ...");
//        await uow.CreateDatabaseAsync();

        Console.WriteLine("Recreating and migrating database ...");
        await uow.MigrateDatabaseAsync();
    }
}

async Task Import()
{
    Console.WriteLine("=====================");
    Console.WriteLine("Import");

    using (var scope = AppService.ServiceProvider!.CreateScope())
    {
        var importService = scope.ServiceProvider.GetRequiredService<IImportService>();
        var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await importService.ImportDbAsync();

        int countResearchProjects = await uow.ResearchProjectRepository.CountAsync();
        int countObservations = await uow.ObservationRepository.CountAsync();
        int countEquipment = await uow.EquipmentRepository.CountAsync();
        int countLocations = await uow.LocationRepository.CountAsync();
        int countSpecies = await uow.SpeciesRepository.CountAsync();

        Console.WriteLine($" {countResearchProjects} research projects stored in DB");
        Console.WriteLine($" {countObservations} observations stored in DB");
        Console.WriteLine($" {countEquipment} equipment items stored in DB");
        Console.WriteLine($" {countLocations} locations stored in DB");
        Console.WriteLine($" {countSpecies} species stored in DB");
    }

    Console.WriteLine($"Import done");
}