using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Controllers;
using server.Models;
using server.Infrastructure;
using server.Models.ViewModels;
using System.Linq;

namespace server.Controllers
{
    public class CheckOutController : BaseController
    {
        public CheckOutController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        public IActionResult Index()
        {
            ViewData["User"] = this_user;
            
			List<CartItem> cart = GetCartItems();

			if (this_user == null || cart.Count <= 0)
			{
				return Redirect("/");
			}
			if (this_user.Role == 2){
				return Redirect("/staff/staff");
			}

			if (this_user.Role == 3){
				return Redirect("/manager/manager/index");
			}

			ViewData["Addresses"] = _context.Address.Where(a => a.Uid == this_user.Uid).ToList();
			

			CartViewModel cartVM = new()
			{
				CartItems = cart,
				GrandTotal = cart.Sum(x => x.Quantity * x.Price)
			};
			return View(cartVM);
        }
		
		[HttpGet]
		[Route("/summary")]
		public IActionResult Summary()
		{
			return View();
		}

		public async Task<IActionResult> NewAddress([Bind("Uid, Ward, Town, District, City, Zipcode")] Address address)
		{
			_context.Add(address);
			await _context.SaveChangesAsync();
			return Redirect("/CheckOut");
		}

		[HttpPost]
		[Route("/CheckOut/ProcessOrder")]
		public async Task<IActionResult> ProcessOrder([Bind("Address_id,Uid, Ward,Town, District, City, Zipcode")] Address address)
		{
			try{
				if (this_user == null )
				{
					ViewData["msg"] = "No user found";
					return Redirect("/");
				}
				if (this_user.Role == 2){
					ViewData["msg"] = "In sufficient privileges.";
					return Redirect("/staff/staff");
				}

				if (this_user.Role == 3){
					ViewData["msg"] = "In sufficient privileges.";
					return Redirect("/manager/manager/index");
				}

				List<CartItem> cart = GetCartItems();

				if (cart.Count <= 0){
					ViewData["msg"] = "Empty cart.";
					return Redirect("/");
				}

				if (address.Address_id == null || address.Address_id == ""){
					address.Address_id = Guid.NewGuid().ToString();
					_context.Add(address);
					await _context.SaveChangesAsync();
				}

				Order order = new Order();

				order.Uid = this_user.Uid;
				order.Address_id = address.Address_id;
				order.Created_at = DateTime.Now;
				order.Updated_at = DateTime.MinValue;
				order.Status = 0;
				order.Total = 0;

				_context.Add(order);
				await _context.SaveChangesAsync();

				foreach (CartItem item in cart){
					Order_detail detail = new Order_detail();
					detail.Order_id = order.Order_id;
					detail.Pid = item.ProductId;
					detail.quantity = item.Quantity;
					detail.total = item.Total;
					order.Total += item.Total;
					var product = _context.Product.FirstOrDefault(p => p.Pid == item.ProductId);
					if (product != null){
						product.Stock -= item.Quantity;
					}
					_context.Add(detail);
					await _context.SaveChangesAsync();
				}

				HttpContext.Session.Remove("Cart");
				TempData["msg"] = "Your order has been created successfully! Please wait for us to get back to you.";
				return RedirectToAction("Summary");
			}
			catch(Exception ex){
				TempData["err"] = ex;
				return RedirectToAction("/Summary");
			}
		}

		public List<CartItem> GetCartItems()
		{
			List<CartItem> cart;
			if (HttpContext.Session.GetJson<List<CartItem>>("Cart") != null)
			{
				cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");
			}
			else
			{
				cart = new List<CartItem>();
				foreach (var item in cart){
					
				}
			}
			return cart;
		}

		[HttpGet]
		[Route("Address/{id}")]
		public ActionResult<Address> GetAddressById(string id)
    	{
			var address = _context.Address.FirstOrDefault(a => a.Address_id == id);
			return Ok(address);
    	}
    }
}
