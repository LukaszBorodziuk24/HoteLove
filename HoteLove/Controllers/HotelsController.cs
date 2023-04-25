using Microsoft.AspNetCore.Mvc;

namespace HoteLove.Controllers
{
    public class HotelsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
