using HoteLove.Models;
using HoteLove.Services.Interfaces;
using HoteLove;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace HoteLove.Services
{
    public class HotelService : IHotelService
    {
        private readonly DbHoteLoveContext _dbContext;

        public HotelService(DbHoteLoveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(HotelModel hotel)
        {
            _dbContext.Add(hotel);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<HotelModel>> GetAll() 
            => await _dbContext.Hotels.ToListAsync();

        public async Task AddComment(CommentModel comment)
        {
            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<HotelModel> GetById(int id) 
            => await _dbContext.Hotels.FindAsync(id);

        public async Task<IEnumerable<CommentModel>> GetCommentsByHotelId(int hotelId) => 
            await _dbContext.Comments.Where(comment => comment.HotelId== hotelId).ToListAsync();

    }
}

