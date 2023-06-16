using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Controllers;


namespace server.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public IActionResult Index()
        {
            ViewData["User"] = this_user;
            return View();
        }
    }
}
