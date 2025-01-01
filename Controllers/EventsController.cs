using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class EtkinlikController : Controller
    {
        private readonly DBContext _context;

        public EtkinlikController(DBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (_context.Etkinlikler == null)
            {
                return NotFound();
            }
            var etkinlikler = _context.Etkinlikler.ToList();
            return View(etkinlikler);
        }

        public IActionResult Details(int id)
        {
            if (_context.Etkinlikler == null)
            {
                return NotFound();
            }
            var etkinlik = _context.Etkinlikler.FirstOrDefault(e => e.ID == id);
            if (etkinlik == null)
            {
                return NotFound();
            }
            return View(etkinlik);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Etkinlik etkinlik)
        {
            if (ModelState.IsValid)
            {
                if (_context.Etkinlikler == null)
                {
                    return Problem("Entity set 'DBContext.Etkinlikler' is null.");
                }
                _context.Etkinlikler.Add(etkinlik);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(etkinlik);
        }
    }
} 