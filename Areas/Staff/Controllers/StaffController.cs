using Microsoft.AspNetCore.Mvc;

namespace server.Areas.Staff.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
