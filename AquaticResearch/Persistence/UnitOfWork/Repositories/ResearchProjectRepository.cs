using Base.Core.Contracts;
using Base.Persistence;
using Core.Contracts.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class ResearchProjectRepository : GenericRepository<ResearchProject>, IResearchProjectRepository
{
    public ResearchProjectRepository(DbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ResearchProject>> GetAll()
    {
        return await Context.Set<ResearchProject>()
            .Include(rp => rp.Observations)
            .ThenInclude(o => o.Location)
            .Include(rp => rp.Observations)
            .ThenInclude(o => o.Equipment)
            .Include(rp => rp.Species)
            .ToListAsync();
    }
}