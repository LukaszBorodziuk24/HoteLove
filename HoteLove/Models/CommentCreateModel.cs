using System.ComponentModel.DataAnnotations;

namespace HoteLove.Models
{
    public class CommentCreateModel
    {
        [Required(ErrorMessage = "Treść komentarza jest wymagana")]
        [MinLength(1, ErrorMessage = "Komentarz nie może być pusty")]
        [MaxLength(500, ErrorMessage = "Komentarz nie może być dłuższy niż 500 znaków")]
        public string Content { get; set; }

        [Required(ErrorMessage = "ID hotelu jest wymagane")]
        [Range(1, int.MaxValue, ErrorMessage = "Nieprawidłowe ID hotelu")]
        public int HotelId { get; set; }
    }
}


