using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using server.Data;
using server.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Cryptography;
using System.Text;

namespace server.Controllers
{

    public class RegisterController : Controller
    {
        
        private readonly serverContext _context;

        public RegisterController(serverContext context)
        {
            _context = context;
        }
        
        [HttpGet]
        [Route("Register")]
        public IActionResult Index()
        {
            var message = TempData["Message"] as string;
            TempData.Remove("Message"); // Remove the message from TempData to prevent it from persisting across requests

            ViewBag.Message = message; // Pass the message to the view

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Register")]
        public async Task<IActionResult> Register([Bind("Username,Password,Email,FirstName,LastName,Phone,Role,Status")] User user)
        {
            if (ModelState.IsValid)
            {
                string id;
                bool duplication;

                do
                {
                    id = Guid.NewGuid().ToString();
                    duplication = _context.User.Any(u => u.Uid == id);
                }
                while (duplication);

                if (_context.User.Any(u => u.Email == user.Email))
                {
                    TempData["msg"] = "Email already taken.";
                    return RedirectToAction("Index", "Register");
                }
                else if (_context.User.Any(u => u.Username == user.Username))
                {
                    TempData["msg"] = "Username already taken.";
                    return RedirectToAction("Index", "Register");
                }

                else{
                    string hashedPassword = HashPassword(user.Password);
                    user.Password = hashedPassword;
                    user.Uid = id;
                    
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    TempData["msg"] = "Registration succeeded!";
                    return RedirectToAction("Index", "Register");
                }
            }
            else
            {
                // Registration failed
                var errorMessages = new List<string>();
                foreach (var modelStateValue in ModelState.Values)
                {
                    foreach (var error in modelStateValue.Errors)
                    {
                        errorMessages.Add(error.ErrorMessage);
                    }
                }
                TempData["msg"] = errorMessages[0];

                return RedirectToAction("Index", "Register");
            }
        }

        private string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }

    }
}
