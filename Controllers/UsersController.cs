using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using server.Data;
using server.Models;

namespace server.Controllers
{
    public class UsersController : Controller
    {
        private readonly serverContext _context;

        public UsersController(serverContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index(string SearchString, string SortOrder)
        {
            //return _context.User != null ?
            //            View(await _context.User.ToListAsync()) :
            //            Problem("Entity set 'serverContext.User'  is null.");


            ViewData["CurrentFilter"] = SearchString;
            var users = from b in _context.User
                        select b;
            if (!String.IsNullOrEmpty(SearchString))
            {
                users = users.Where(b => b.Username.Contains(SearchString));
            }

            //Sort by Status
            //users = users.OrderBy(b => b.Username);
            ViewData["UsernameSort"] = string.IsNullOrEmpty(SortOrder) ? "username_sort" : "";
            ViewData["StatusSort"] = SortOrder == "Status" ? "status_sort" : "status_sort";

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

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
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

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Uid,Username,Password,Email,FirstName,LastName,Phone,Role,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.User == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            // Set the select list for the role
            ViewData["RoleList"] = new SelectList(new[]
            {
        new { Value = "Staff", Text = "Staff" },
        new { Value = "Customer", Text = "Customer" }
    }, "Value", "Text", user.Role);

            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Uid,Username,Role")] User user)
        {
            if (id != user.Uid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Uid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            // Set the select list for the role
            ViewData["RoleList"] = new SelectList(new[]
            {
        new { Value = "Staff", Text = "Staff" },
        new { Value = "Customer", Text = "Customer" }
    }, "Value", "Text", user.Role);
            return View(user);
        }

        // GET: Users/Suspend/5
        public async Task<IActionResult> Suspend(int? id)
        {
            if (id == null || _context.User == null)
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
        public async Task<IActionResult> SuspendConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            if (user.Status.Equals("Suspended"))
            {
                user.Status = "Online";
            }
            else
            {
                user.Status = "Suspended";
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_context.User?.Any(e => e.Uid == id)).GetValueOrDefault();
        }
    }
}
