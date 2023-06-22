using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using server.Controllers;
using server.Data;
using server.Models;

namespace server.Areas.Staff.Controllers
{
    public class ChartData
    {
        public string[] Labels { get; set; }
        public int[] Data { get; set; }
    }


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
            if (this_user.Role != 2)
            {
				return Redirect("/");
			}
			ViewData["User"] = this_user;

            var orders = _context.Order.Where(o => o.Status == 1).ToList();

            ViewData["Orders"] = orders;

            var chartData = new ChartData
            {
                Labels = new[] { "Label 1", "Label 2", "Label 3" },
                Data = new[] { 10, 20, 30 }
            };

            return View(chartData);
        }
    }
}