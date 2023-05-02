using HoteLove.Models;
using Microsoft.EntityFrameworkCore;

namespace HoteLove.Services.Interfaces
{
    public interface IHotelService
    {
        Task Create(HotelModel hotel);
        Task<IEnumerable<HotelModel>> GetAll();
    }
}
