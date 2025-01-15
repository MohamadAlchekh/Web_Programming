#nullable enable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace FinalProject.Models
{
    public class ToplulukOlusturmaIstegi
    {
        [Key]
        public int ID { get; set; }

        [Required(ErrorMessage = "Topluluk adı zorunludur")]
        [Display(Name = "Topluluk Adı")]
        [StringLength(100, MinimumLength = 2)]
        public string ToplulukAdi { get; set; } = "";

        [Required(ErrorMessage = "Başkan adı ve soyadı zorunludur")]
        [Display(Name = "Başkan Adı Soyadı")]
        [StringLength(100, MinimumLength = 5)]
        public string BaskanAdSoyad { get; set; } = "";

        [Display(Name = "Başkanlık Belgesi")]
        [NotMapped]
        public IFormFile? KanitBelgesi { get; set; }

        [Display(Name = "Belge Yolu")]
        public string? KanitBelgesiYolu { get; set; }

        [Display(Name = "Ek Açıklama")]
        [StringLength(1000)]
        public string? Aciklama { get; set; }

        [Required]
        public DateTime BasvuruTarihi { get; set; } = DateTime.Now;

        [Required]
        public ToplulukBasvuruDurumu Durum { get; set; } = ToplulukBasvuruDurumu.Beklemede;
    }

    public enum ToplulukBasvuruDurumu
    {
        Beklemede, // 0
        Onaylandi, // 1
        Reddedildi // 2
    }
} 