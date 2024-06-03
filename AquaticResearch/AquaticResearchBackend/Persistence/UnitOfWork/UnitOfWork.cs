using Base.Persistence;
using Core.Contracts;
using Core.Contracts.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.UnitOfWork;

public class UnitOfWork : BaseUnitOfWork, IUnitOfWork
{
    public IEquipmentRepository EquipmentRepository { get; }
    public ILocationRepository LocationRepository { get; }
    public IObservationRepository ObservationRepository { get; }
    public IResearchProjectRepository ResearchProjectRepository { get; }
    public ISpeciesRepository SpeciesRepository { get; }

    public UnitOfWork(ApplicationDbContext dbContext,
        IEquipmentRepository equipmentRepository,
        ILocationRepository locationRepository,
        IObservationRepository observationRepository,
        IResearchProjectRepository researchProjectRepository,
        ISpeciesRepository speciesRepository
    ) : base(dbContext)
    {
        this.EquipmentRepository = equipmentRepository;
        this.LocationRepository = locationRepository;
        this.ObservationRepository = observationRepository;
        this.ResearchProjectRepository = researchProjectRepository;
        this.SpeciesRepository = speciesRepository;
    }
}