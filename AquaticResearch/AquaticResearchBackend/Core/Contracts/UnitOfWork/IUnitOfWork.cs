using Base.Core.Contracts;
using Core.Contracts.Repositories;

namespace Core.Contracts;

public interface IUnitOfWork : IBaseUnitOfWork
{
    public IEquipmentRepository EquipmentRepository { get; }
    public ILocationRepository LocationRepository { get; }
    public IObservationRepository ObservationRepository { get; }
    public IResearchProjectRepository ResearchProjectRepository { get; }
    public ISpeciesRepository SpeciesRepository { get; }
}