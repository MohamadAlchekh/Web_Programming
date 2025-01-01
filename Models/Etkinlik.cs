using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FinalProject.Models
{
    public class Etkinlik
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 5)]
        public string Baslik { get; set; } = "";
        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public string Aciklama { get; set; } = "";
        [Required]
        public DateTime Tarih { get; set; }
        [Required]
        [StringLength(300, MinimumLength = 2)]
        public string Lokasyon { get; set; } = "";
        [Required]
        [ForeignKey("Topluluk")]
        public int Topluluk { get; set; }  // FK to Topluluk
        [Required]
        public int KatilimSayisi { get; set; }
        [Required]
        public bool Online { get; set; }
        [Required]
        [Url]
        public string ResimUrl { get; set; } = "";
    }
} 