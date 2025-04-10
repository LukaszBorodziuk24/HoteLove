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
            var userId = _userManager.GetUserId(User);
            var hotels = _managementService.GetHotelsByUserId(userId);

            return View(hotels);
        }

        //Kontroler widoku Edit, pobieramy z bazy danych poprzednie dane i wyswietlamy je w formularzu tak
        //by użytkownik wiedział co zmienia
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _userManager.GetUserId(User);
            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);
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

        //Zapis nowych danych do bazy
        [HttpPost]
        public async Task<IActionResult> Edit(HotelModel model)
        {
            var userId = _userManager.GetUserId(User);
            var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == model.Id && h.UserId == userId);

            hotel.Description = model.Description;
            hotel.Price = model.Price;
            hotel.PhoneNumber = model.PhoneNumber;
            hotel.Address = model.Address;
            hotel.Email = model.Email;

            //Pomicięcie w walidacji pól których nie przypisujemy w formularzu
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

        //Usuwanie hotelu
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

