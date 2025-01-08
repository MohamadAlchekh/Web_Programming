using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using FinalProject.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.Extensions.Options;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {

        private readonly DBContext _context;

        public AccountController(DBContext context)
        {
            _context = context;
        }
        [HttpPost]
        public IActionResult Login(string Email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Email);

            if (user != null)
            {
                var passwordHasher = new PasswordHasher<User>();
                var result = passwordHasher.VerifyHashedPassword(user, user.SifreHash, password);
                
                if (result == PasswordVerificationResult.Success)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Email),
                        new Claim(ClaimTypes.Role, user.Role),
                    };
                    var identity = new ClaimsIdentity(claims, "CookieAuth");
                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync("CookieAuth", principal);
                    return RedirectToAction("Index", "Home");
                }
            }
            
            ViewBag.Error = "Kullanıcı adı veya parola hatalı.";
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            
            if (ModelState.IsValid) 
            {
                var SifreHash = new PasswordHasher<User>();

                user.SifreHash = SifreHash.HashPassword(user, user.SifreHash);

                _context.Users.Add(user);
               _context.SaveChanges();
               TempData["SuccessMessage"] = "Registration successful! Please log in.";

                return RedirectToAction("login");
            }
                return View("Index");
        }

    }
}
