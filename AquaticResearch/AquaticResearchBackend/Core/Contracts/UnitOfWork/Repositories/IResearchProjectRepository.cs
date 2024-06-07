using Base.Core.Contracts;
using Core.Entities;
using Core.Entities.DTO.DTOs;

namespace Core.Contracts.UnitOfWork.Repositories;

public interface IResearchProjectRepository : IGenericRepository<ResearchProject>
{
    public Task<IEnumerable<ResearchProjectDto>> GetAllResearchProjectDTOs(); 
}