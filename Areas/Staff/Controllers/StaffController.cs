using Microsoft.AspNetCore.Mvc;

namespace server.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {
        [Area("Staff")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
