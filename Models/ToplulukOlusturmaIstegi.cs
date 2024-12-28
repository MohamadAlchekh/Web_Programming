using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Models;

public class ToplulukOlusturmaIstegi
{
    [Required(ErrorMessage = "Topluluk adı zorunludur")]
    [Display(Name = "Topluluk Adı")]
    public string ToplulukAdi { get; set; } = "";

    [Required(ErrorMessage = "Başkan adı ve soyadı zorunludur")]
    [Display(Name = "Başkan Adı Soyadı")]
    public string BaskanAdSoyad { get; set; } = "";

    [Display(Name = "Başkanlık Belgesi")]
    public IFormFile? KanitBelgesi { get; set; }
    
    
    [Display(Name = "Ek Açıklama")]
    public string? Aciklama { get; set; }

    public DateTime BasvuruTarihi { get; set; } = DateTime.Now;
    public ToplulukBasvuruDurumu Durum { get; set; } = ToplulukBasvuruDurumu.Beklemede;
}

public enum ToplulukBasvuruDurumu
{
    Beklemede,
    Onaylandi,
    Reddedildi
} 