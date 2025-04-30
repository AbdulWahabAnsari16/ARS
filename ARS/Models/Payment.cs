using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public int ReservationId { get; set; }
        [ForeignKey("ReservationId")]
        public Reservation Reservation { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string CardNumberMasked { get; set; }
        public string Type { get; set; }
        public string TransactionRef { get; set; }
    }
}
