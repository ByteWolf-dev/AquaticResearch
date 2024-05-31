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
}