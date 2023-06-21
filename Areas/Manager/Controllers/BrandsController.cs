﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using server.Models;
using server.Data;
using server.Controllers;

namespace server.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BrandsController : BaseController
    {
        public BrandsController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {
        }

        // GET: Manager/Brands
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
            return _context.Brand != null ?
                        View(await _context.Brand.ToListAsync()) :
                        Problem("Entity set 'serverContext.Brand'  is null.");
        }

        // GET: Manager/Brands/Details/5
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
            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .FirstOrDefaultAsync(m => m.Bid == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }



        // GET: Manager/Brands/Delete/5
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
            if (id == null || _context.Brand == null)
            {
                return NotFound();
            }

            var brand = await _context.Brand
                .FirstOrDefaultAsync(m => m.Bid == id);
            if (brand == null)
            {
                return NotFound();
            }

            return View(brand);
        }

        // POST: Manager/Brands/Delete/5
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
            if (_context.Brand == null)
            {
                return Problem("Entity set 'serverContext.Brand'  is null.");
            }
            var brand = await _context.Brand.FindAsync(id);
            if (brand != null)
            {
                _context.Brand.Remove(brand);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BrandExists(int id)
        {
            return (_context.Brand?.Any(e => e.Bid == id)).GetValueOrDefault();
        }
    }
}
