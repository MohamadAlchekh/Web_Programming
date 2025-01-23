using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class Topluluk
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Isim { get; set; } = "";
        [Required]
        [StringLength(5000, MinimumLength = 10)]
        public string Aciklama { get; set; } = "";
        [Required]
        [StringLength(100, MinimumLength = 2)]
        public string Universite { get; set; } = "";
        [Required]
        public int UyeSayisi { get; set; }
        [Required]
        [ForeignKey("User")]
        public int Olusturan { get; set; }
        [Required]
        [Url]
        public string ResimUrl { get; set; } = "";
        [Required]
        [Url]
        public string LogoUrl { get; set; } = "";
        [Required]
        public bool Onayli { get; set; }
        public virtual ICollection<Etkinlik> Etkinlikler { get; set; } = new List<Etkinlik>();
        public virtual ICollection<Katilim> Katilimlar { get; set; } = new List<Katilim>();
    }
} 