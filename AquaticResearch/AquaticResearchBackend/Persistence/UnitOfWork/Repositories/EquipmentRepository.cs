using Base.Persistence;
using Core.Contracts.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class EquipmentRepository : GenericRepository<Equipment>, IEquipmentRepository
{
    public EquipmentRepository(DbContext context) : base(context)
    {
    }
}