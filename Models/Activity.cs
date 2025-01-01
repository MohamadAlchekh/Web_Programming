using System.ComponentModel.DataAnnotations;

namespace FinalProject.Models
{
    public class Activity
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public string Type { get; set; } = "";
        [Required]
        public DateTime Date { get; set; }
    }
} 