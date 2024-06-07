using Base.Core.Contracts;
using Core.Entities;
using Core.Entities.DTO.DTOs;

namespace Core.Contracts.UnitOfWork.Repositories;

public interface IObservationRepository : IGenericRepository<Observation>
{
    public Task<IEnumerable<ObservationDto>> GetObservationsByResearchProjectTitle(string title);
}