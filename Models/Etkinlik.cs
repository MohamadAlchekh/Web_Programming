using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

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
        [ForeignKey("ToplulukEntity")]
        public int Topluluk { get; set; }  // FK to Topluluk
        public virtual Topluluk ToplulukEntity { get; set; }
        [Required]
        public int KatilimSayisi { get; set; }
        [Required]
        public int MaksimumKatilimci { get; set; }
        [Required]
        public bool Online { get; set; }
        [Required]
        [Url]
        public string ResimUrl { get; set; } = "";
        public virtual ICollection<EtkinlikKatilim> Katilimlar { get; set; } = new List<EtkinlikKatilim>();
    }
} 