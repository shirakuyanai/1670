using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Infranstructure;
using server.Infranstructure.Components;
using server.Models;
using server.Models.ViewModels;

namespace server.Controllers
{
    public class CartController : Controller
    {
        private readonly serverContext _context;
        public CartController(serverContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart")??new List<CartItem>();
            CartViewModel cartVM = new()
            {
                CartItems = cart,
                GrandTotal = cart.Sum(x => x.Quantity * x.Price)
            };
            return View(cartVM);
        }
    }
}
