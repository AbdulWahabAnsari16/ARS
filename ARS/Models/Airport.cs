using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class Airport
    {
        [Key]
        public int AirportId { get; set; }
        public string Name { get; set; }
        public string IATACode { get; set; }

        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

        // Navigation properties for routes
        public ICollection<FlightRoutes> OriginRoutes { get; set; }
        public ICollection<FlightRoutes> DestinationRoutes { get; set; }

        // Other navigation properties
        public ICollection<FlightSchedule> DepartureSchedules { get; set; }
        public ICollection<FlightSchedule> ArrivalSchedules { get; set; }
    }
}