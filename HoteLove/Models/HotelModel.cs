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
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa hotelu musi mieć od 3 do 50 znaków")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Lokalizacja jest wymagana")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Nazwa miejscowosci musi mieć od 3 do 50 znaków")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Krótki opis jest wymagany")]
        [StringLength(250, MinimumLength = 10, ErrorMessage = "Opis musi mieć od 10 do 250 znaków")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Podanie ceny jest konieczne")]
        [RegularExpression(@"^\d{1,5}(\.\d{1,2})?$", ErrorMessage = "Cena musi być liczbą z maksymalnie 5 cyframi i do 2 miejscami po przecinku.")]
        public string Price { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [RegularExpression(@"\d{3}-\d{3}-\d{3}", ErrorMessage = "Numer telefonu musi mieć format 123-123-123")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Adres musi mieć od 3 do 50 znaków")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format emaila")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "email moze miec od 5 do 50 znaków")]
        public string Email { get; set; }
        

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public IdentityUser User { get; set; }






    }
}
