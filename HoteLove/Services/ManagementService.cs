using HoteLove.Models;
using HoteLove.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HoteLove.Services
{
    public class ManagementService : IManagementService
    {
        private readonly DbHoteLoveContext _dbContext;
        public ManagementService(DbHoteLoveContext dbHoteLoveContext)
        {
            _dbContext = dbHoteLoveContext;
        }
        public List<HotelModel> GetHotelsByUserId(string userId)
            => _dbContext.Hotels.Where(h => h.UserId == userId).ToList();
    }
}
