using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly DBContext _context;

    public HomeController(ILogger<HomeController> logger, DBContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var yayindakiEtkinlikler = await _context.Etkinlikler
            .Where(e => e.Tarih >= DateTime.Now)
            .OrderBy(e => e.Tarih)
            .ToListAsync();

        var toplulukIds = yayindakiEtkinlikler.Select(e => e.Topluluk).Distinct().ToList();
        var topluluklar = await _context.Topluluklar
            .Where(t => toplulukIds.Contains(t.ID))
            .ToDictionaryAsync(t => t.ID, t => t.Isim);

        foreach (var topluluk in topluluklar)
        {
            ViewData["Topluluk_" + topluluk.Key] = topluluk.Value;
        }

        return View(yayindakiEtkinlikler);
    }

    public async Task<IActionResult> About()
    {
        ViewBag.ToplulukSayisi = await _context.Topluluklar.CountAsync();
        ViewBag.EtkinlikSayisi = await _context.Etkinlikler.CountAsync();
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Contact()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
