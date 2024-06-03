using Base.Persistence;
using Core.Contracts.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class LocationRepository : GenericRepository<Location>, ILocationRepository
{
    public LocationRepository(DbContext context) : base(context)
    {
    }
}