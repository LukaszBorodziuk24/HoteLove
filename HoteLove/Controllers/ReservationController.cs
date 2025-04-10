using HoteLove.Models;
using HoteLove.Services;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        //Wyświetlane są tylko te hotele które dany użytkownik "Regular_User" zarezerwował
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
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var reservation = new ReservationModel
            {
                HotelId = hotelId,
                UserId = userId,
                ReservationDate = DateTime.Now,

            };

            _reservationService.Create(reservation);

            return RedirectToAction("Index");
        }

        //Usuwanie rezerwacji
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
