using HoteLove.Models;
using HoteLove.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoteLove.Services
{
    public class RatingService : IRatingService
    {
        private readonly DbHoteLoveContext _dbContext;
        public RatingService(DbHoteLoveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(RatingModel rating)
        {
            _dbContext.Add(rating);
            await _dbContext.SaveChangesAsync();
        }

        public double CalculateAverageRating(int hotelId)
        {
            var ratings = _dbContext.Ratings.Where(r => r.HotelId == hotelId).ToList();
            if (ratings.Count == 0)
            {
                return 0;
            }

            double sum = ratings.Sum(r => r.Value);
            double average = sum / ratings.Count;
            average = Math.Round(average, 1);

            return average;
        }

        public bool HasUserRatedHotel(int hotelId, string userId) 
            => _dbContext.Ratings.Any(r => r.HotelId == hotelId && r.UserId == userId);
    }
}
