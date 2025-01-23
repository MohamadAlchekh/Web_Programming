using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.IO;

namespace FinalProject.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly DBContext _context;
        private readonly IWebHostEnvironment _environment;

        public AdminController(DBContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new AdminDashboardViewModel
            {
                TopluluklarSayisi = await _context.Topluluklar.CountAsync(),
                EtkinliklerSayisi = await _context.Etkinlikler.CountAsync(),
                BekleyenIstekler = await _context.ToplulukOlusturmaIstekleri
                    .Where(t => t.Durum == ToplulukBasvuruDurumu.Beklemede)
                    .ToListAsync()
            };

            return View(viewModel);
        }

        public async Task<IActionResult> TumTopluluklar()
        {
            var topluluklar = await _context.Topluluklar
                .OrderByDescending(t => t.UyeSayisi)
                .ToListAsync();
            return View(topluluklar);
        }

        public async Task<IActionResult> TumEtkinlikler()
        {
            var etkinlikler = await _context.Etkinlikler
                .OrderByDescending(e => e.Tarih)
                .ToListAsync();
            return View(etkinlikler);
        }

        public async Task<IActionResult> TumUyeler()
        {
            var uyeler = await _context.Users
                .OrderBy(u => u.İsimSoyisim)
                .ToListAsync();
            return View(uyeler);
        }

        public async Task<IActionResult> IstekDetay(int id)
        {
            var istek = await _context.ToplulukOlusturmaIstekleri.FindAsync(id);
            if (istek == null)
            {
                return NotFound();
            }
            return View(istek);
        }

        public async Task<IActionResult> IstekOnayla(int id)
        {
            var istek = await _context.ToplulukOlusturmaIstekleri.FindAsync(id);
            if (istek == null)
            {
                return NotFound();
            }

            istek.Durum = ToplulukBasvuruDurumu.Onaylandi;
            await _context.SaveChangesAsync();

            var yeniTopluluk = new Topluluk
            {
                Isim = istek.ToplulukAdi,
                Aciklama = istek.Aciklama ?? "",
                UyeSayisi = 1,
                Olusturan = istek.OlusturanId,
                Universite = istek.Universite ?? "Belirtilmemiş",
                ResimUrl = "/images/default-community.jpg",
                LogoUrl = istek.LogoYolu ?? "/images/default-logo.png",
                Onayli = true
            };

            _context.Topluluklar.Add(yeniTopluluk);
            await _context.SaveChangesAsync();

            var katilim = new Katilim
            {
                Kullanici = istek.OlusturanId,
                Topluluk = yeniTopluluk.ID,
                KatilmaTarihi = DateTime.Now
            };
            _context.Katilimlar.Add(katilim);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"{istek.ToplulukAdi} isimli topluluk başarıyla onaylanmıştır.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> IstekReddet(int id)
        {
            var istek = await _context.ToplulukOlusturmaIstekleri.FindAsync(id);
            if (istek == null)
            {
                return NotFound();
            }

            istek.Durum = ToplulukBasvuruDurumu.Reddedildi;
            await _context.SaveChangesAsync();

            if (!string.IsNullOrEmpty(istek.KanitBelgesiYolu))
            {
                var filePath = Path.Combine(_environment.WebRootPath, istek.KanitBelgesiYolu.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            if (!string.IsNullOrEmpty(istek.LogoYolu))
            {
                var logoPath = Path.Combine(_environment.WebRootPath, istek.LogoYolu.TrimStart('/'));
                if (System.IO.File.Exists(logoPath))
                {
                    System.IO.File.Delete(logoPath);
                }
            }

            TempData["Success"] = $"{istek.ToplulukAdi} isimli topluluk talebi reddedildi.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> TumIstekler()
        {
            var istekler = await _context.ToplulukOlusturmaIstekleri
                .OrderByDescending(i => i.BasvuruTarihi)
                .ToListAsync();
            return View(istekler);
        }

        public async Task<IActionResult> EtkinlikDetay(int id)
        {
            var etkinlik = await _context.Etkinlikler
                .Include(e => e.ToplulukEntity)
                .FirstOrDefaultAsync(e => e.ID == id);
            if (etkinlik == null)
            {
                return NotFound();
            }
            return View(etkinlik);
        }

        public async Task<IActionResult> TumSilmeIslemleri(string type)
        {
            var viewModel = new AdminSilmeViewModel();
            
            if (string.IsNullOrEmpty(type) || type == "topluluk")
            {
                viewModel.Topluluklar = await _context.Topluluklar
                    .OrderBy(t => t.Isim)
                    .ToListAsync();
            }
            
            if (string.IsNullOrEmpty(type) || type == "etkinlik")
            {
                viewModel.Etkinlikler = await _context.Etkinlikler
                    .OrderBy(e => e.Baslik)
                    .ToListAsync();
            }
            
            ViewBag.SelectedType = type;
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Sil(int id, string type)
        {
            if (type == "topluluk")
            {
                var topluluk = await _context.Topluluklar.FindAsync(id);
                if (topluluk != null)
                {
                    _context.Topluluklar.Remove(topluluk);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Topluluk başarıyla silindi.";
                }
            }
            else if (type == "etkinlik")
            {
                var etkinlik = await _context.Etkinlikler.FindAsync(id);
                if (etkinlik != null)
                {
                    _context.Etkinlikler.Remove(etkinlik);
                    await _context.SaveChangesAsync();
                    TempData["Success"] = "Etkinlik başarıyla silindi.";
                }
            }
            
            return RedirectToAction("TumSilmeIslemleri", new { type = type });
        }
    }
}