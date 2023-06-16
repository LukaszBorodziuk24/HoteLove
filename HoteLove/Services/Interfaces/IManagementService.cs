

using HoteLove.Models;

namespace HoteLove.Services.Interfaces
{
    public interface IManagementService
    {
        List<HotelModel> GetHotelsByUserId(string userId);
    }
}
