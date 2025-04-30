using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARS.Models
{
    public class UserProfile
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string PreferredCard { get; set; }
        public int SkyMiles { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
    }
}
