using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Controllers;
using server.Models;
using server.Infranstructure;

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
            var products = _context.Product.ToList();
            ViewData["Products"] = products;
            var brands = _context.Brand.ToList();
            ViewData["Brands"] = brands;
            ViewData["CartItems"] = GetCartItems();
            return View();
        }

        private List<CartItem> GetCartItems()
        {
            List<CartItem> cart;
            if (HttpContext.Session.GetJson<List<CartItem>>("Cart") != null)
            {
                cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
            }
            else
            {
                cart = new List<CartItem>();
            }
            return cart;
        }
    }
}
