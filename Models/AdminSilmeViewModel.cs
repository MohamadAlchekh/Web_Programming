using System.Collections.Generic;

namespace FinalProject.Models
{
    public class AdminSilmeViewModel
    {
        public List<Topluluk> Topluluklar { get; set; }
        public List<Etkinlik> Etkinlikler { get; set; }

        public AdminSilmeViewModel()
        {
            Topluluklar = new List<Topluluk>();
            Etkinlikler = new List<Etkinlik>();
        }
    }
} 