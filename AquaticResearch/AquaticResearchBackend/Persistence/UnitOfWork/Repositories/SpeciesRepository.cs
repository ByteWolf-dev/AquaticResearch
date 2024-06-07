using Base.Persistence;
using Core.Contracts.UnitOfWork.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class SpeciesRepository : GenericRepository<Species>, ISpeciesRepository
{
    public SpeciesRepository(DbContext context) : base(context)
    {
    }
}