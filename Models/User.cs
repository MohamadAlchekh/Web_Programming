using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string KullaniciAdi { get; set; } = "";
        [Required]
        [StringLength(70, MinimumLength = 5)]
        public string İsimSoyisim { get; set; } = "";
        [Required]
        [EmailAddress]
        public string Email { get; set; } = "";
        [Required]
        public string SifreHash { get; set; } = "";
        [Required]
        public DateTime KayitTarihi { get; set; } = DateTime.Now;
        public string KatildigiEtkinlikler { get; set; } = "";
        public string TakipEttigiTopluluklar { get; set; } = "";
    }
} 