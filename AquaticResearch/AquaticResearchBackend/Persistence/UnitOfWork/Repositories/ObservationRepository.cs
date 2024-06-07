using AquaticResearch.DTO;
using Base.Persistence;
using Core.Contracts.UnitOfWork.Repositories;
using Core.Entities;
using Core.Entities.DTO.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class ObservationRepository : GenericRepository<Observation>, IObservationRepository
{
    public ObservationRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ObservationDto>> GetObservationsByResearchProjectTitle(string title)
    {
        
        var observations = await Context.Set<Observation>()
            .Include(o => o.ResearchProject)
            .Include(o => o.Equipment)
            .Include(o => o.Location)
            .ToListAsync();

        var observationsWithMatchingTitle = observations.Where(o => o.ResearchProject.Title == title).ToList();
        return observationsWithMatchingTitle.Select(o => o.ToDto());
    }
}