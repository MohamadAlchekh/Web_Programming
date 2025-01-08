using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

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

    public IActionResult Olustur()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Olustur(ToplulukOlusturmaIstegi model)
    {
        if (!ModelState.IsValid)
            return View(model);

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
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Liste()
    {
        var topluluklar = _context.Topluluklar
            .OrderByDescending(t => t.UyeSayisi)
            .ToList();
        return View(topluluklar);
    }
} 