using System.ComponentModel.DataAnnotations;

namespace HoteLove.Models
{
    public class RegisterHotel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
