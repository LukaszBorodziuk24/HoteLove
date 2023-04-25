using Microsoft.AspNetCore.Mvc;

namespace HoteLove.Controllers
{
    public class ManagementController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
