using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace FinalProject.Controllers
{
    public class EventsController : Controller
    {
        private readonly DBContext _context;

        public EventsController(DBContext context)
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

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userIdClaim))
            {
                var userId = int.Parse(userIdClaim);
                var isToplulukBaskani = _context.Topluluklar
                    .Any(t => t.Onayli && t.Olusturan == userId);
                
                ViewBag.IsToplulukBaskani = isToplulukBaskani;
            }
            else
            {
                ViewBag.IsToplulukBaskani = false;
            }

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

        [Authorize]
        public IActionResult Create()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var topluluklar = _context.Topluluklar
                .Where(t => t.Onayli && t.Olusturan == userId)
                .OrderBy(t => t.Isim)
                .Select(t => new SelectListItem
                {
                    Value = t.ID.ToString(),
                    Text = t.Isim
                })
                .ToList();

            if (!topluluklar.Any())
            {
                TempData["Error"] = "Etkinlik oluşturmak için bir topluluğun başkanı olmanız gerekmektedir.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Topluluklar = topluluklar;
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult Create(Etkinlik etkinlik)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var topluluk = _context.Topluluklar.FirstOrDefault(t => t.ID == etkinlik.Topluluk);

            if (topluluk == null || topluluk.Olusturan != userId)
            {
                TempData["Error"] = "Bu topluluk için etkinlik oluşturma yetkiniz bulunmamaktadır.";
                return RedirectToAction(nameof(Index));
            }

            if (ModelState.IsValid)
            {
                if (_context.Etkinlikler == null)
                {
                    return Problem("Entity set 'DBContext.Etkinlikler' is null.");
                }
                
                if (etkinlik.MaksimumKatilimci <= 0)
                {
                    ModelState.AddModelError("MaksimumKatilimci", "Maksimum katılımcı sayısı 0'dan büyük olmalıdır.");
                    var kullanicininTopluluklari = _context.Topluluklar
                        .Where(t => t.Onayli && t.Olusturan == userId)
                        .OrderBy(t => t.Isim)
                        .Select(t => new SelectListItem
                        {
                            Value = t.ID.ToString(),
                            Text = t.Isim
                        })
                        .ToList();
                    ViewBag.Topluluklar = kullanicininTopluluklari;
                    return View(etkinlik);
                }

                etkinlik.KatilimSayisi = 0;
                _context.Etkinlikler.Add(etkinlik);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            var topluluklar = _context.Topluluklar
                .Where(t => t.Onayli && t.Olusturan == userId)
                .OrderBy(t => t.Isim)
                .Select(t => new SelectListItem
                {
                    Value = t.ID.ToString(),
                    Text = t.Isim
                })
                .ToList();

            ViewBag.Topluluklar = topluluklar;
            return View(etkinlik);
        }

        public IActionResult BiletAl(int id)
        {
            var etkinlik = _context.Etkinlikler.FirstOrDefault(e => e.ID == id);

            if (etkinlik == null)
            {
                return NotFound();
            }


            TempData["Message"] = "Bilet alma işleminiz başarıyla tamamlandı!";
            return RedirectToAction(nameof(Details), new { id = etkinlik.ID });
        }

        [HttpPost]
        public IActionResult EtkinligeKatil(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            var mevcutKatilim = _context.EtkinlikKatilimlar
                .FirstOrDefault(k => k.EtkinlikID == id && k.KullaniciID == userId);

            if (mevcutKatilim != null)
            {
                return Json(new { type = "warning", message = "Bu etkinliğe zaten katıldınız!" });
            }

            var etkinlik = _context.Etkinlikler.Find(id);
            if (etkinlik == null)
            {
                return Json(new { type = "danger", message = "Etkinlik bulunamadı!" });
            }

            var yeniKatilim = new EtkinlikKatilim
            {
                EtkinlikID = id,
                KullaniciID = userId,
                KatilmaTarihi = DateTime.Now
            };

            _context.EtkinlikKatilimlar.Add(yeniKatilim);
            etkinlik.KatilimSayisi++;
            _context.SaveChanges();

            return Json(new { type = "success", message = "Etkinliğe başarıyla katıldınız!" });
        }
    }
} 