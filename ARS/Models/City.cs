using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class City
    {
        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }   
        public string Country { get; set; }
        //public string StateProvince { get; set; }
        //public double? Latitude { get; set; }
        //public double? Longitude { get; set; }

        public ICollection<Airport> airports { get; set; }
    }
}
