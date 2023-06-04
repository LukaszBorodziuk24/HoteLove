using HoteLove;
using HoteLove.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

[Authorize(Roles = "Hotel_User")]
public class ManagementController : Controller
{
    private readonly DbHoteLoveContext _dbContext;
    private readonly UserManager<IdentityUser> _userManager;

    public ManagementController(DbHoteLoveContext dbContext, UserManager<IdentityUser> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public IActionResult Index()
    {
        // Pobierz id aktualnie zalogowanego użytkownika
        var userId = _userManager.GetUserId(User);

        // Pobierz hotele utworzone przez danego użytkownika
        var hotels = _dbContext.Hotels.Where(h => h.UserId == userId).ToList();

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

        if (hotel == null)
        {
            return NotFound();
        }

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

        if (hotel == null)
        {
            return NotFound();
        }

        // Zaktualizuj tylko zmienne pola
        hotel.Description = model.Description;
        hotel.Price = model.Price;
        hotel.PhoneNumber = model.PhoneNumber;
        hotel.Address = model.Address;
        hotel.Email = model.Email;

        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        // Pobierz id aktualnie zalogowanego użytkownika
        var userId = _userManager.GetUserId(User);

        // Sprawdź, czy dany hotel należy do zalogowanego użytkownika
        var hotel = await _dbContext.Hotels.FirstOrDefaultAsync(h => h.Id == id && h.UserId == userId);

        if (hotel == null)
        {
            return NotFound();
        }

        _dbContext.Hotels.Remove(hotel);
        await _dbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }

}
