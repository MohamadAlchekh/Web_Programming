using FinalProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
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

            // Yeni topluluk oluştur
            var yeniTopluluk = new Topluluk
            {
                Isim = istek.ToplulukAdi,
                Aciklama = istek.Aciklama ?? "",
                UyeSayisi = 1,
                Olusturan = 1, // TODO: Gerçek user ID'si ile değiştirilmeli
                Universite = "Üniversite Adı", // TODO: Üniversite bilgisi eklenebilir
                ResimUrl = "/images/default-community.jpg", // Varsayılan resim
                Onayli = true
            };

            _context.Topluluklar.Add(yeniTopluluk);
            await _context.SaveChangesAsync();

            TempData["Success"] = $"{istek.ToplulukAdi} isimli topluluk başarıyla oluşturuldu.";
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

            // Eğer belge varsa sil
            if (!string.IsNullOrEmpty(istek.KanitBelgesiYolu))
            {
                var filePath = Path.Combine(_environment.WebRootPath, istek.KanitBelgesiYolu.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
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
                .Include(e => e.Topluluk)
                .FirstOrDefaultAsync(e => e.ID == id);
            if (etkinlik == null)
            {
                return NotFound();
            }
            return View(etkinlik);
        }
    }
}