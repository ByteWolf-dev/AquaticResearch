using Base.Core;
using Base.Tools.CsvImport;
using Core.Contracts;
using Core.Entities;
using ImportConsoleApp.CSVEntities;
using Microsoft.Extensions.DependencyInjection;

namespace ImportConsoleApp;

public class ImportService : IImportService
{
    public async Task ImportDbAsync()
    {
        //Read in all files
        var csvResearchProjects = await new CsvImport<CsvResearchProject>()
            .ReadAsync("ImportData/ResearchProjects.csv");

        var csvObservations = await new CsvImport<CsvObservation>()
            .ReadAsync("ImportData/Observations.csv");

        var csvLocations = await new CsvImport<Location>().ReadAsync("ImportData/Locations.csv");
        var csvEquipment = await new CsvImport<Equipment>().ReadAsync("ImportData/Equipment.csv");
        var csvSpecies = await new CsvImport<Species>().ReadAsync("ImportData/Species.csv");


        using (var scope = AppService.ServiceProvider!.CreateAsyncScope())
        {
            var uow = AppService.ServiceProvider!.GetRequiredService<IUnitOfWork>();

            var locationsInDb = await uow.LocationRepository.GetAsync();
            var locationsNotInDb = csvLocations
                .Where(l => locationsInDb.All(db => db.Name != l.Name))
                .DistinctBy(e => e.Name)
                .ToList();

            var equipmentInDb = await uow.EquipmentRepository.GetAsync();
            var equipmentNotInDb = csvEquipment
                .Where(e => equipmentInDb.All(db => db.Name != e.Name))
                .DistinctBy(e => e.Name)
                .ToList();

            var speciesInDb = await uow.SpeciesRepository.GetAsync();
            var speciesNotInDb = csvSpecies
                .Where(s => speciesInDb.All(db => db.ScientificName != s.ScientificName))
                .DistinctBy(s => s.ScientificName)
                .ToList();

            var researchProjectsInDb = await uow.ResearchProjectRepository.GetAsync();
            var researchProjectsNotInDb = csvResearchProjects
                .Where(rp => researchProjectsInDb.All(db => db.Title != rp.Title))
                .DistinctBy(rp => rp.Title)
                .ToList();

            
            var combinedSpecies = speciesInDb.Union(speciesNotInDb);
            List<ResearchProject> newResearchProjects = new List<ResearchProject>();

            researchProjectsNotInDb.ForEach(rp =>
            {
                newResearchProjects.Add(
                    new ResearchProject()
                    {
                        Title = rp.Title,
                        Species = combinedSpecies.FirstOrDefault(s => s.ScientificName == rp.Species)
                    }
                );
            });

            var combinedResearchProjects = researchProjectsInDb.Union(newResearchProjects);
            var combinedEquipment = equipmentInDb.Union(equipmentNotInDb);
            var combinedLocations = locationsInDb.Union(locationsNotInDb);
            List<Observation> newObservations = new List<Observation>();
            
            foreach (var csvObservation in csvObservations)
            {
                var csvObservationEquipmentSplit = csvObservation.Equipment.Split(',');
                var equipment = combinedEquipment.Where(ce => csvObservationEquipmentSplit.Contains(ce.Name)).ToList();
                var location = combinedLocations.FirstOrDefault(l => csvObservation.Location == l.Name);
                var researchProject = combinedResearchProjects.FirstOrDefault(rp => csvObservation.ResearchProject == rp.Title);
                
                var newObservation = new Observation()
                {
                    Notes = csvObservation.Notes,
                    Researchers = csvObservation.Researchers.Split(',').ToList(),
                    ObservationDateTime = csvObservation.ObservationDateTime,
                    Equipment = equipment,
                    Location = location,
                    ResearchProject = researchProject
                };
                newObservations.Add(newObservation);
            }
            
            
            await uow.LocationRepository.AddRangeAsync(locationsNotInDb);
            await uow.EquipmentRepository.AddRangeAsync(equipmentNotInDb);
            await uow.SpeciesRepository.AddRangeAsync(speciesNotInDb);
            await uow.ResearchProjectRepository.AddRangeAsync(newResearchProjects);
            await uow.ObservationRepository.AddRangeAsync(newObservations);
            await uow.SaveChangesAsync();
        }
    }
}