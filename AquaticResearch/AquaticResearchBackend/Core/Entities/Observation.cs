using Base.Core.Entities;

namespace Core.Entities;

public class Observation : EntityObject
{
    public string Notes { get; set; }
    public DateTime ObservationDateTime { get; set; }
    public List<string> Researchers { get; set; } = new List<string>();
    public List<Equipment> Equipment { get; set; } = new List<Equipment>();
    public Location Location { get; set; }
    public ResearchProject ResearchProject { get; set; }
}