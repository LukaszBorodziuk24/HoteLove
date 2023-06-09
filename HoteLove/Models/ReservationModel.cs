using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HoteLove.Models
{
    public class ReservationModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime ReservationDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int HotelId { get; set; }

    }
}
