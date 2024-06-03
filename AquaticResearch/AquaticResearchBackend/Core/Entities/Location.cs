using Base.Core.Entities;

namespace Core.Entities
{
    public class Location : EntityObject
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
