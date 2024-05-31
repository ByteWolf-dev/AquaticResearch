using Base.Core.Entities;

namespace Core.Entities;

public class ResearchProject : EntityObject
{
    public string Title { get; set; }
    public Species Species { get; set; }
    public List<Observation> Observations { get; set; } = new List<Observation>();
}