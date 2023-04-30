using System.ComponentModel.DataAnnotations;

namespace HoteLove.Models
{
    public class LoginUser
    {
        [Required]
        public string UserName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public UserType UserType { get; set; }

    }

    public enum UserType
    {
        RegularUser,
        HotelUser
    }
}
