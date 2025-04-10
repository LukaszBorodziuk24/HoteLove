using HoteLove.Models;
using HoteLove.Services.Interfaces;

namespace HoteLove.Services
{
    public class ReservationService : IReservationService
    {
        private readonly DbHoteLoveContext _dbContext;

        public ReservationService(DbHoteLoveContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsHotelAlreadyReserved(string userId, int hotelId) 
            => _dbContext.Reservations.Any(r => r.UserId == userId && r.HotelId == hotelId);

        public void Create(ReservationModel reservation)
        {
            if (!IsHotelAlreadyReserved(reservation.UserId, reservation.HotelId))
            {
                _dbContext.Reservations.Add(reservation);
                _dbContext.SaveChanges();
            }
            
        }

        public void Delete(ReservationModel reservation)
        {
            _dbContext.Reservations.Remove(reservation);
            _dbContext.SaveChanges();
        }

        public List<ReservationModel> GetReservationsByUser(string userId) 
            => _dbContext.Reservations.Where(r => r.UserId == userId).ToList();

        public List<ReservationModel> GetReservationsByHotel(int hotelId) 
            => _dbContext.Reservations.Where(r => r.HotelId == hotelId).ToList();

        public List<HotelModel> GetHotelsByIds(IEnumerable<int> hotelIds) 
            => _dbContext.Hotels.Where(h => hotelIds.Contains(h.Id)).ToList();

        public ReservationModel GetReservation(string userId, int hotelId)
            => _dbContext.Reservations.FirstOrDefault(r => r.UserId == userId && r.HotelId == hotelId);
    }

}
