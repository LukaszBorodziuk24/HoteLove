using HoteLove.Models;
using HoteLove.Services;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoteLove.Controllers
{
    [Authorize(Roles = "Hotel_User")]
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;
        private readonly IUserContext _userContext;

        public HotelsController(IHotelService hotelService, IUserContext userContext)
        {
            _hotelService = hotelService;
            _userContext = userContext;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotelModel hotel)
        {
            hotel.UserId = _userContext.GetUserId();
            await _hotelService.Create(hotel);
            return RedirectToAction("Index","Home");
        }
    }
}