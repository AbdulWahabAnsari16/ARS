using System.ComponentModel.DataAnnotations;

namespace ARS.Models
{
    public class verificationCode
    {
        [Key]
        public int vCodeId { get; set; }
        public string vCode { get; set; }
    }
}
