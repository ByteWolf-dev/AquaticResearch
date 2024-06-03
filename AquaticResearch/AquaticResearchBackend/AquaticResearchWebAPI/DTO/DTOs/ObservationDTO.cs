using Core.Entities;

namespace AquaticResearch.DTO.DTOs;

public class ObservationDto
{
    public string Notes { get; set; }
    public DateTime ObservationDateTime { get; set; }
    public string Researchers { get; set; }
    public string Equipment { get; set; }
    public string Location { get; set; }
    
}