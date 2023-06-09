using HoteLove.Models;

namespace HoteLove.Services.Interfaces
{
    public interface IReservationService
    {
        public bool IsHotelAlreadyReserved(string userId, int hotelId);
        public void Create(ReservationModel reservation);
        public void Delete(ReservationModel reservation);
        public ReservationModel GetReservation(string userId, int hotelId);
        public List<ReservationModel> GetReservationsByUser(string userId);
        public List<ReservationModel> GetReservationsByHotel(int hotelId);
        public List<HotelModel> GetHotelsByIds(IEnumerable<int> hotelIds);

    }
}
