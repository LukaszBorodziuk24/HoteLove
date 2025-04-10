using HoteLove.Models;
using HoteLove.Services;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HoteLove.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILogger<HomeController> _logger;
        private readonly IHotelService _hotelService;
        private readonly IRatingService _ratingService;
        private readonly IUserContext _userContext;


        public HomeController(ILogger<HomeController> logger, IHotelService hotelService, IRatingService ratingService, IUserContext userContext)
        {
            _logger = logger;
            _hotelService = hotelService;
            _ratingService = ratingService;
            _userContext = userContext;
        }

        //Metoda pobierająca wszystkie istniejące hotele z bazy danych
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

        //Odpowiednio zabezpieczona metoda do tworzenia komentarzy, tylko dla użytkowników "Regular_User"
        [HttpPost]
        [Authorize(Roles = "Regular_User")]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = new CommentModel
            {
                Content = model.Content,
                CreatedAt = DateTime.Now,
                HotelId = model.HotelId
            };

            await _hotelService.AddComment(comment);

            return Json(new { success = true });
        }


        //Metoda pobierająca wszystkie komentarze przypisane do id danego hotelu
        public async Task<IActionResult> Details(int id)
        {
            var hotel = await _hotelService.GetById(id);
            var comments = await _hotelService.GetCommentsByHotelId(id);

            var viewModel = new ViewModel
            {
                Hotel = hotel,
                Comments = comments
            };

            return View(viewModel);
        }

        //Akcja kontrolera tworząca nową ocene dla hotelu, jest tu także sprawdzenie czy użytkownik juz przypadkiem 
        //nie dodał oceny w takim przypadku ocena nie zostaje dodana
        [HttpPost]
        [Authorize(Roles = "Regular_User")]
        public async Task<IActionResult> CreateRating([FromBody] RatingCreateModel ratingCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { error = "Nieprawidłowe dane wejściowe." });
            }

            string userId = _userContext.GetUserId();

            if (_ratingService.HasUserRatedHotel(ratingCreate.HotelId, userId))
            {
                return Conflict(new { error = "Użytkownik już ocenił ten hotel." });
            }

            RatingModel newRating = new RatingModel
            {
                HotelId = ratingCreate.HotelId,
                UserId = userId,
                Value = ratingCreate.Value
            };

            await _ratingService.Create(newRating);

            var newAverageRating = _ratingService.CalculateAverageRating(ratingCreate.HotelId);

            return Ok(new { newAverageRating });
        }





    }
}