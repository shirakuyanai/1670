using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using server.Controllers;
using server.Data;
using server.Models;

namespace server.Areas.Staff.Controllers
{
	[Area("Staff")]
	public class OrdersController : BaseController
	{
		public OrdersController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
		{
			
		}

		// GET: Staff/Orders
		public async Task<IActionResult> Index()
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
			var serverContext = _context.Order.Include(o => o.Address).Include(o => o.User);
			return View(await serverContext.ToListAsync());
		}



		// GET: Staff/Orders/Edit/5
		public async Task<IActionResult> Edit(string id)
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
			if (id == null || _context.Order == null)
			{
				return NotFound();
			}

			var order = await _context.Order.FindAsync(id);
			if (order == null)
			{
				return NotFound();
			}
			ViewData["Address_id"] = new SelectList(_context.Address, "Address_id", "Address_id", order.Address_id);
			ViewData["Uid"] = new SelectList(_context.User, "Uid", "Uid", order.Uid);
			return View(order);
		}

		// POST: Staff/Orders/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, [Bind("Order_id,Uid,Address_id,Created_at,Updated_at,Status,Total")] Order order)
		{
			if (id != order.Order_id)
			{
				return NotFound();
			}

			try
			{
				_context.Update(order);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!OrderExists(order.Order_id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			ViewData["Address_id"] = new SelectList(_context.Address, "Address_id", "Address_id", order.Address_id);
			ViewData["Uid"] = new SelectList(_context.User, "Uid", "Uid", order.Uid);
			return Redirect("/staff/orders");
		}
		private bool OrderExists(string id)
		{
			return (_context.Order?.Any(e => e.Order_id == id)).GetValueOrDefault();
		}
	}
}
