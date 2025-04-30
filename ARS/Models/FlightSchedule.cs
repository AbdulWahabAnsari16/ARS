using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class FlightSchedule
    {
        [Key]
        public int ScheduleID { get; set; }

        public int FlightId { get; set; }
        [ForeignKey("FlightId")]
        public Flight Flight { get; set; }

        // Departure Airport Relationship
        public int DepartureAirportId { get; set; }
        [ForeignKey("DepartureAirportId")]
        public Airport DepartureAirport { get; set; }  // Renamed from "Airport"

        // Arrival Airport Relationship
        public int ArrivalAirportId { get; set; }
        [ForeignKey("ArrivalAirportId")]
        public Airport ArrivalAirport { get; set; }  // Fixed typo ("Airpor" -> "ArrivalAirport")

        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public int ClassId { get; set; }
        [ForeignKey("ClassId")]
        public Class Class { get; set; }
        public int SeatsAvailable { get; set; }

        public List<ItinerarySegment> Segments { get; set; } = new();
    }
}