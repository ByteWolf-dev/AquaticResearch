﻿using Base.Persistence;
using Core.Contracts.Repositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork.Repositories;

public class ObservationRepository : GenericRepository<Observation>, IObservationRepository
{
    public ObservationRepository(DbContext context) : base(context)
    {
    }
}