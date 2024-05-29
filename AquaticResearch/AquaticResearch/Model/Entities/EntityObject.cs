using AquaticResearch.Model.Contracts;

namespace AquaticResearch.Model.Entities;

public class EntityObject : IEntityObject
{
    public int Id { get; set; }
    public byte[]? RowVersion { get; set; }
}