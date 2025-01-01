using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly DBContext _context;

        public ProfileController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = _context.Users.FirstOrDefault();
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
    }
} 