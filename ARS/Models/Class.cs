using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class Class
    {
        [Key]
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public string? Description { get; set; }
        public ICollection<FlightSchedule> Flights { get; set; }
        public ICollection<PricingRule> Rules { get; set; }
    }
}
