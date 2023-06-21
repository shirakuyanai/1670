using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Controllers;
using server.Data;
using server.Models;

namespace server.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ManagerController : BaseController
    {

        public ManagerController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {

        }

        public IActionResult Index()
        {
            Console.WriteLine(this_user.Role);
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            ViewData["User"] = this_user;
            return View();
        }
    }
}