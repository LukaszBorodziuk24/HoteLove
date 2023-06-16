using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HoteLove.Models
{
    public class HotelModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa jest wymagana")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Krótki opis jest wymagany")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Podanie ceny jest konieczne")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        public string Email { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }






    }
}
