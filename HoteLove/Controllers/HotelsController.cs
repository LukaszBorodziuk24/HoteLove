using HoteLove.Models;
using HoteLove.Services;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoteLove.Controllers
{
    public class HotelsController : Controller
    {
        private readonly IHotelService _hotelService;


        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet]
        [Authorize(Roles = "Hotel_User")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Hotel_User")]
        public async Task<IActionResult> Create(HotelModel hotel)
        {
            await _hotelService.Create(hotel);
            return RedirectToAction(nameof(Create));
        }
    }
}