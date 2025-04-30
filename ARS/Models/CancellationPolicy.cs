using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class CancellationPolicy
    {
        [Key]
        public int PolicyId { get; set; }
        public int MinDaysBefore { get; set; }
        public int MaxDaysBefore { get; set; }
        public decimal RefundPercent { get; set; }
    }
}
