using HoteLove.Models;
using HoteLove.Services;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HoteLove.Controllers
{
    [Authorize(Roles = "Regular_User")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly IHotelService _hotelService;
        private readonly IUserContext _userContext;

        public ReservationController(IReservationService reservationService, IHotelService hotelService, IUserContext userContext)
        {
            _reservationService = reservationService;
            _hotelService = hotelService;
            _userContext = userContext;
        }

        public IActionResult Index()
        {
            var userId = _userContext.GetUserId();
            var reservations = _reservationService.GetReservationsByUser(userId);
            var hotelIds = reservations.Select(r => r.HotelId).Distinct();
            var hotels = _reservationService.GetHotelsByIds(hotelIds);
            return View(hotels);
        }

        [HttpPost]
        public IActionResult Create(int hotelId)
        {
            var userId = _userContext.GetUserId();

            var reservation = new ReservationModel
            {
                HotelId = hotelId,
                UserId = userId,
                ReservationDate = DateTime.Now,

                // Ustawienie innych właściwości rezerwacji, jeśli są wymagane
            };

            _reservationService.Create(reservation);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(int hotelId)
        {
            var userId = _userContext.GetUserId();
            var reservation = _reservationService.GetReservation(userId, hotelId);

            if (reservation == null)
            {
                // Rezerwacja nie istnieje, zwróć odpowiedni widok lub przekierowanie
                return RedirectToAction("Index");
            }

            _reservationService.Delete(reservation);

            return RedirectToAction("Index");
        }
    }
}
