using HoteLove.Models;
using HoteLove.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.Linq;



namespace HoteLove.Controllers
{
    [Authorize(Roles = "Hotel_User")]
    public class ManagementController : Controller
    {
        private readonly DbHoteLoveContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IManagementService _managementService;

        public ManagementController(DbHoteLoveContext dbContext, UserManager<IdentityUser> userManager, IManagementService managementService)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _managementService = managementService;
        }

        public IActionResult Index()
        {
            // Pobierz id aktualnie zalogowanego użytkownika
            var userId = _userManager.GetUserId(User);

            // Pobierz hotele utworzone przez danego użytkownika
            var hotels = _managementService.GetHotelsByUserId(userId);

            // Przekazuj hotele do widoku w celu wyświetlenia

            return View(hotels);
        }

        // Inne metody akcji

        public async Task<IActionResult> Edit(int id)
        {
            // Pobierz id aktualnie zalogowanego użytkownika
            var userId = _userManager.GetUserId(User);

            // Sprawdź, czy dany hotel należy do zalogowanego użytkownika
            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);


            // Przekazuj hotel do widoku w celu edycji, przy zachowaniu niezmienialnych pól
            var model = new HotelModel
            {
                Description = hotel.Description,
                Price = hotel.Price,
                PhoneNumber = hotel.PhoneNumber,
                Address = hotel.Address,
                Email = hotel.Email
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(HotelModel model)
        {
            // Pobierz id aktualnie zalogowanego użytkownika
            var userId = _userManager.GetUserId(User);

            // Sprawdź, czy dany hotel należy do zalogowanego użytkownika
            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == model.Id && h.UserId == userId);


            // Zaktualizuj tylko zmienne pola
            hotel.Description = model.Description;
            hotel.Price = model.Price;
            hotel.PhoneNumber = model.PhoneNumber;
            hotel.Address = model.Address;
            hotel.Email = model.Email;


            ModelState["User"]!.ValidationState = ModelValidationState.Valid;
            ModelState["UserId"]!.ValidationState = ModelValidationState.Valid;
            ModelState["Name"]!.ValidationState = ModelValidationState.Valid;
            ModelState["Location"]!.ValidationState = ModelValidationState.Valid;


            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = _userManager.GetUserId(User);

            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);


            _dbContext.Hotels.Remove(hotel);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction("Index");
        }

    }
}

