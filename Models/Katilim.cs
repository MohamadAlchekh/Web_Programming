using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace FinalProject.Models
{
    public class Katilim
    {
        [Key]
        public int ID { get; set; }
        [Required]
        [ForeignKey("User")]
        public int Kullanici { get; set; } 
        [Required]
        [ForeignKey("Topluluk")]
        public int Topluluk { get; set; }  
        [Required]
        public DateTime KatilmaTarihi { get; set; }
    }
} 