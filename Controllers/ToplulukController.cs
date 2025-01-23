using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FinalProject.Controllers;

public class ToplulukController : Controller
{
    private readonly IWebHostEnvironment _environment;
    private readonly DBContext _context;

    public ToplulukController(IWebHostEnvironment environment, DBContext context)
    {
        _environment = environment;
        _context = context;
    }

    [Authorize]
    public IActionResult Olustur()
    {
        return View();
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Olustur(ToplulukOlusturmaIstegi model)
    {
        
        if (!ModelState.IsValid)
            return View(model);

        // Kullanıcı ID'sini al
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        model.OlusturanId = userId;

        // Logo yükleme kontrolü ve işlemi
        if (model.Logo != null)
        {
            // Logo boyutu kontrolü (2MB)
            if (model.Logo.Length > 2 * 1024 * 1024)
            {
                ModelState.AddModelError("Logo", "Logo boyutu 2MB'dan büyük olamaz.");
                return View(model);
            }

            // Logo uzantısı kontrolü
            var allowedLogoExtensions = new[] { ".jpg", ".jpeg", ".png" };
            var logoExtension = Path.GetExtension(model.Logo.FileName).ToLowerInvariant();
            
            if (!allowedLogoExtensions.Contains(logoExtension))
            {
                ModelState.AddModelError("Logo", "Logo için sadece JPG ve PNG dosyaları yüklenebilir.");
                return View(model);
            }

            // Logo dosyasını kaydet
            var logoFileName = $"logo_{Guid.NewGuid()}{logoExtension}";
            var logoFilePath = Path.Combine(_environment.WebRootPath, "uploads", "logos", logoFileName);

            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "uploads", "logos"));
            
            using (var stream = new FileStream(logoFilePath, FileMode.Create))
            {
                await model.Logo.CopyToAsync(stream);
            }

            // Logo yolunu modele kaydet
            model.LogoYolu = $"/uploads/logos/{logoFileName}";
        }
        else
        {
            ModelState.AddModelError("Logo", "Topluluk logosu zorunludur.");
            return View(model);
        }

        // Eğer dosya yüklendiyse kontrolleri yap
        if (model.KanitBelgesi != null)
        {
            // Dosya boyutu kontrolü
            if (model.KanitBelgesi.Length > 5 * 1024 * 1024) // 5MB
            {
                ModelState.AddModelError("KanitBelgesi", "Dosya boyutu 5MB'dan büyük olamaz.");
                return View(model);
            }

            // Dosya uzantısı kontrolü
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
            var fileExtension = Path.GetExtension(model.KanitBelgesi.FileName).ToLowerInvariant();
            
            if (!allowedExtensions.Contains(fileExtension))
            {
                ModelState.AddModelError("KanitBelgesi", "Sadece JPG, PNG ve PDF dosyaları yüklenebilir.");
                return View(model);
            }

            // Dosyayı kaydet
            var fileName = $"{Guid.NewGuid()}{fileExtension}";
            var filePath = Path.Combine(_environment.WebRootPath, "uploads", fileName);

            Directory.CreateDirectory(Path.Combine(_environment.WebRootPath, "uploads"));
            
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await model.KanitBelgesi.CopyToAsync(stream);
            }

            // Dosya yolunu modele kaydet
            model.KanitBelgesiYolu = $"/uploads/{fileName}";
        }

        // Veritabanına kaydet
        _context.ToplulukOlusturmaIstekleri.Add(model);
        await _context.SaveChangesAsync();

        TempData["Success"] = "Topluluk oluşturma talebiniz alınmıştır. İnceleme sonrası size bilgi verilecektir.";
        return RedirectToAction("Liste", "Topluluk");
    }

    public IActionResult Liste()
    {
        var topluluklar = _context.Topluluklar
            .OrderByDescending(t => t.UyeSayisi)
            .ToList();
        return View(topluluklar);
    }

    public IActionResult Detay(int id)
    {
        var topluluk = _context.Topluluklar
            .Include(t => t.Etkinlikler)
            .FirstOrDefault(t => t.ID == id);
            
        if (topluluk == null)
        {
            return NotFound();
        }

        // Etkinlikleri tarihe göre sırala
        topluluk.Etkinlikler = topluluk.Etkinlikler
            .OrderByDescending(e => e.Tarih)
            .ToList();

        return View(topluluk);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Katil(int id)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        // Topluluğun var olup olmadığını kontrol et
        var topluluk = await _context.Topluluklar.FindAsync(id);
        if (topluluk == null)
        {
            return Json(new { type = "danger", message = "Topluluk bulunamadı!" });
        }

        // Kullanıcının zaten üye olup olmadığını kontrol et
        var mevcutKatilim = await _context.Katilimlar
            .FirstOrDefaultAsync(k => k.Topluluk == id && k.Kullanici == userId);

        if (mevcutKatilim != null)
        {
            return Json(new { type = "warning", message = "Bu topluluğa zaten üyesiniz!" });
        }

        // Yeni katılım oluştur
        var katilim = new Katilim
        {
            Topluluk = id,
            Kullanici = userId,
            KatilmaTarihi = DateTime.Now
        };

        _context.Katilimlar.Add(katilim);
        topluluk.UyeSayisi++;
        await _context.SaveChangesAsync();

        return Json(new { type = "success", message = "Topluluğa başarıyla katıldınız!" });
    }
} 