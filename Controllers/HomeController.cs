using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using server.Controllers;
using server.Models;
using server.Infrastructure;

namespace server.Controllers
{
    public class HomeController : BaseController
    {
        private readonly EmailSender _emailSender;

        public HomeController(serverContext context, IHttpContextAccessor httpContextAccessor, EmailSender emailSender) : base(context, httpContextAccessor)
        {
            _emailSender = emailSender;
        }

        public List<Product> BestSeller()
        {
            List<Order_detail> orderDetailsList = _context.Order_detail.ToList();
            var sortedList = orderDetailsList
                .GroupBy(o => o.Pid)
                .OrderByDescending(g => g.Count())
                .SelectMany(g => g)
                .ToList();
            
            List<Product> products = new List<Product>();
            
            foreach (var item in sortedList)
            {
                Product product = _context.Product.SingleOrDefault(p => p.Pid == item.Pid);
                
                if (product != null)
                {
                    if (!products.Contains(product))
                    {
                        products.Add(product);
                    }
                }
            }
            
            return products;
        }


        public IActionResult Index()
        {
            ViewData["User"] = this_user;
            var products = _context.Product.ToList();
            ViewData["Products"] = products;

            var brands = _context.Brand.ToList();
            ViewData["Brands"] = brands;
            ViewData["CartItems"] = GetCartItems();

            List<Product> bestseller = BestSeller();
            ViewData["BestSeller"] = bestseller;
            
            return View();
        }

        [HttpGet]
        [Route("/product/{id}")]
        public async Task<IActionResult> Product(int id)
        {
			ViewData["User"] = this_user;
			var brands = _context.Brand.ToList();
            ViewData["Brands"] = brands;
			var product = _context.Product.FirstOrDefault(p => p.Pid == id);
			product.viewCount++;
			ViewData["Product"] = _context.Product.FirstOrDefault(p => p.Pid == id);
			await _context.SaveChangesAsync();
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

        



        // GET: Home/Search
        public async Task<IActionResult> Search(string SearchProduct)
        {
            ViewData["User"] = this_user;
            var brands = _context.Brand.ToList();
            ViewData["Brands"] = brands;
            ViewData["CurrentFilter"] = SearchProduct;
            var products = _context.Product.AsQueryable();

            if (!string.IsNullOrEmpty(SearchProduct))
            {
                products = products.Where(p => p.Name.Contains(SearchProduct));
            }

            return View(await products.ToListAsync());
        }


        // GET: Home/SideMenuBar
        public IActionResult SideMenuBar(int? Bid)
        {
            ViewData["User"] = this_user;
            var brands = _context.Brand.ToList();
            ViewData["Brands"] = brands;
            var products = _context.Product.ToList();
            ViewData["Products"] = products;

            if (Bid.HasValue)
            {
                products = products.Where(p => p.Bid == Bid.Value).ToList();
                ViewData["p.Brand"] = _context.Brand.FirstOrDefault(b => b.Bid == Bid)?.Title;
            }
            else
            {
                ViewData["Brand"] = "All Brands";
            }
            return View(products);
        }





    }
}
