using HoteLove.Models;
using HoteLove.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

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
        {
            return await _dbContext.Hotels.ToListAsync();
        }
    }
}
