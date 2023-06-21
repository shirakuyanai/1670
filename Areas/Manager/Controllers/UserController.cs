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
    public class UsersController : BaseController
    {

        public UsersController(serverContext context, IHttpContextAccessor httpContextAccessor) : base(context, httpContextAccessor)
        {

        }

        // GET: Manager/Users
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
            ViewData["User"] = this_user;

            return _context.User != null ?
                        View(await _context.User.ToListAsync()) :
                        Problem("Entity set 'serverContext.User'  is null.");
        }
        // GET: Manager/Users/Search
        public async Task<IActionResult> Search(string SearchString) {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            ViewData["CurrentFilter"] = SearchString;
            var users = from user in _context.User
                        select user;
            if (!string.IsNullOrEmpty(SearchString))
            {
                users = users.Where(b => b.Username.Contains(SearchString));
            }
            return View(users);
        }


        // GET: Manager/Users/Sort
        public async Task<IActionResult> Sort(string SortOrder)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            ViewData["UsernameSort"] = string.IsNullOrEmpty(SortOrder) ? "username_sort" : "";
            ViewData["StatusSort"] = SortOrder == "Status" ? "status_sort" : "status_sort";
            var users = from user in _context.User
                        select user;

            switch (SortOrder)
            {
                case "username_sort":
                default:
                    users = users.OrderBy(b => b.Username);
                    break;
                case "status_sort":
                    users = users.OrderBy(b => b.Status);
                    break;
            }
            return View(users);
        }


        // GET: Manager/Users/Details/5
        public async Task<IActionResult> Details(string? id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.Uid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Manager/Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
			ViewData["User"] = this_user;
			if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Manager/Users/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Uid,Username,Role,Email")] User user)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            if (id != user.Uid)
            {
                return NotFound();
            }

            // Retrieve the existing user from the database
            var existingUser = await _context.User.FindAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }

            // Update the properties of the existing user
            existingUser.Username = user.Username;
            existingUser.Role = user.Role;
            existingUser.Email = user.Email; // Make sure to set the email property

            _context.Update(existingUser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        //Uid,Username,Password,Email,Firstname,LastName,Phone,Role,Status,Verifed


        // GET: Users/Suspend/5
        public async Task<IActionResult> Suspend(string id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var user = await _context.User.FirstOrDefaultAsync(m => m.Uid == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Suspend/5
        [HttpPost, ActionName("Suspend")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuspendConfirmed(string id)
        {
            if (this_user == null)
            {
                return Redirect("/");
            }
            if (this_user.Role != 3)
            {
                return Redirect("/");
            }
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Status == false)
            {
                user.Status = true;
            }
            else
            {
                user.Status = false;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        private bool UserExists(string id)
        {
            return (_context.User?.Any(e => e.Uid == id)).GetValueOrDefault();
        }

    }
}