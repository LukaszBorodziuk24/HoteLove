using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HoteLove.Models
{
    public class RatingModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        public string UserId { get; set; }

        [Range(1, 5)]
        public double Value { get; set; }
    }
}
