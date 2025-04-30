using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }
        public int ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AgeCategory { get; set; }
    }
}
