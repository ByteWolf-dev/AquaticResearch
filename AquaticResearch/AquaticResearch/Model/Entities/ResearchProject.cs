namespace AquaticResearch.Model.Entities;

public class ResearchProject : EntityObject
{
    public string Title { get; set; }
    public Species Species { get; set; }
    public List<Observation> Observations { get; set; }
}