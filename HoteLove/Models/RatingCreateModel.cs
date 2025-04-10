
using System.ComponentModel.DataAnnotations;

namespace HoteLove.Models
{
    public class RatingCreateModel
    {
        public int HotelId { get; set; }
        public int Value { get; set; }
    }
}
