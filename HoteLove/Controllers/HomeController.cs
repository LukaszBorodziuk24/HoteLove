using HoteLove.Models;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HoteLove.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHotelService _hotelService;


        public HomeController(ILogger<HomeController> logger, IHotelService hotelService)
        {
            _logger = logger;
            _hotelService = hotelService;
        }

        public async Task<IActionResult> Index()
        {
            var Hotels = await _hotelService.GetAll();
            var viewModels = new List<ViewModel>();

            foreach (var hotel in Hotels)
            {
                var comments = await _hotelService.GetCommentsByHotelId(hotel.Id);

                var viewModel = new ViewModel
                {
                    Hotel = hotel,
                    Comments = comments
                };

                viewModels.Add(viewModel);
            }


            return View(viewModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment(int hotelId, string content)
        {
            // Pobierz hotel o danym Id
            var hotel = await _hotelService.GetById(hotelId);
            if (hotel == null)
            {
                return NotFound();
            }

            // Utwórz nowy komentarz
            var comment = new CommentModel
            {
                Content = content,
                CreatedAt = DateTime.Now,
                HotelId = hotelId
            };

            // Dodaj komentarz do bazy danych
            await _hotelService.AddComment(comment);

            // Przekieruj użytkownika na stronę z listą hoteli
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            // Pobierz hotel o danym Id
            var hotel = await _hotelService.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }

            // Pobierz komentarze dla danego hotelu
            var comments = await _hotelService.GetCommentsByHotelId(id);

            // Utwórz ViewModel zawierający hotel i komentarze
            var viewModel = new ViewModel
            {
                Hotel = hotel,
                Comments = comments
            };

            return View(viewModel);
        }



    }
}