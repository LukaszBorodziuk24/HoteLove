namespace HoteLove.Models
{
    public class ViewModel
    {
        public HotelModel Hotel { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
    }
}
