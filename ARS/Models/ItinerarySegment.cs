using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class ItinerarySegment
    {
        [Key]
        public int SegmentID { get; set; }

        public int ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }

        public int ScheduleId { get; set; }
        [ForeignKey("ScheduleId")]
        public FlightSchedule Schedule { get; set; }

        public int SegmentNumber { get; set; }
    }
}
