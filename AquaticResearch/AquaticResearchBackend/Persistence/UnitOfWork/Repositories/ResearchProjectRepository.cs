using AquaticResearch.DTO;
using Base.Core.Contracts;
using Base.Persistence;
using Core.Contracts.UnitOfWork.Repositories;
using Core.Entities;
using Core.Entities.DTO.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class ResearchProjectRepository : GenericRepository<ResearchProject>, IResearchProjectRepository
{
    public ResearchProjectRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ResearchProjectDto>> GetAllResearchProjectDTOs()
    {
        var researchProjects = await Context.Set<ResearchProject>()
            .Include(rp => rp.Species).ToListAsync();

        return researchProjects.Select<ResearchProject, ResearchProjectDto>(p => p.ToDto());
    }
}