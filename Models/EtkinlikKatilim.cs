using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.Models
{
    public class EtkinlikKatilim
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [ForeignKey("User")]
        public int KullaniciID { get; set; }
        public virtual User User { get; set; }

        [Required]
        [ForeignKey("Etkinlik")]
        public int EtkinlikID { get; set; }
        public virtual Etkinlik Etkinlik { get; set; }

        [Required]
        public DateTime KatilmaTarihi { get; set; } = DateTime.Now;
    }
} 