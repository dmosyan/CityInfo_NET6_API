using CityInfo.API.Models;

namespace CityInfo.API.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
            = new List<PointOfInterestDto>();

        public City(string name)
        {
            Name = name;
        }
    }
}
