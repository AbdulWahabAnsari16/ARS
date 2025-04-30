using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class Flight
    {
        [Key]
        public int FlightId { get; set; }
        public string FlightNumber { get; set; }
        public string AircraftType { get; set; }

        //public TimeSpan BaseDuration { get; set; } // Minutes

        public List<FlightSchedule> Schedules { get; set; }
    }
}
