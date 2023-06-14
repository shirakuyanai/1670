using Microsoft.AspNetCore.Mvc;

namespace project16_5_2023.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
