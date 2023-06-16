using HoteLove.Models;

namespace HoteLove.Services.Interfaces
{
    public interface IRatingService
    {
        Task Create(RatingModel rating);
        double CalculateAverageRating(int hotelId);
        bool HasUserRatedHotel(int hotelId, string userId);
    }
}
