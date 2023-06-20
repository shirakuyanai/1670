using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Controllers;
using server.Data;
using server.Models;

namespace server.Areas.Staff.Controllers
{
	[Area("Staff")]
	public class StaffController : BaseController
    {

        public StaffController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {

        }
        
        public IActionResult Index()
        {
			if (this_user == null)
			{
				return Redirect("/");
			}
			ViewData["User"] = this_user;
            return View();
        }
    }
}