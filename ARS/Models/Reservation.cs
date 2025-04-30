using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }

        public string Type { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateOnly? ExpiryDate { get; set; }
        public string BlockingNumber { get; set; }
        public string ConfirmationNumber { get; set; }

        public List<Passenger> Passengers { get; set; }
        public List<ItinerarySegment> Segments { get; set; }
        public List<Payment> Payments { get; set; }
        public ICollection<MileageHistory> History { get; set; }

    }
}
