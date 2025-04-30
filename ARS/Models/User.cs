using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsGuest { get; set; }
        public DateTime CreatedAt { get; set; }
        public ICollection<UserProfile> Profile { get; set; }
        public List<Reservation> Reservations { get; set; }
        public ICollection<MileageHistory> MileageHistories { get; set; }
    }
}
