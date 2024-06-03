using Base.Core.Entities;

namespace Core.Entities;

public class Species : EntityObject
{
    public string Name { get; set; }
    public string ScientificName { get; set; }
}