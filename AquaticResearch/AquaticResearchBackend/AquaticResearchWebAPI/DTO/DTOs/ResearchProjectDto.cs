using Core.Entities;

namespace AquaticResearch.DTO.DTOs;

public class ResearchProjectDto
{
    public string Title { get; set; }
    public string Species { get; set; }
    public List<ObservationDto> ObservationDtos { get; set; } = new List<ObservationDto>();
}