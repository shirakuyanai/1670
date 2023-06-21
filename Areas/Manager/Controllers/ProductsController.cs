using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Data;
using Microsoft.AspNetCore.Http;
using server.Controllers;

namespace server.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ProductsController : BaseController
    {
        public ProductsController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        // GET: Manager/Products
        public async Task<IActionResult> Index()
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            var serverContext = _context.Product.Include(p => p.Brand);
            return View(await serverContext.ToListAsync());
        }

        // GET: Manager/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }



        // GET: Manager/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Brand)
                .FirstOrDefaultAsync(m => m.Pid == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Manager/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            if (_context.Product == null)
            {
                return Problem("Entity set 'serverContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.Pid == id)).GetValueOrDefault();
        }
    }
}
