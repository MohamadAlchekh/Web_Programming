using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        
       
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
        
        [Required]
        public string Role { get; set; } = "User";
    }
} 