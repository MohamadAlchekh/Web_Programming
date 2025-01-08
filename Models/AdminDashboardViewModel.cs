 using System.Collections.Generic;

namespace FinalProject.Models
{
    public class AdminDashboardViewModel
    {
        public int TopluluklarSayisi { get; set; }
        public int EtkinliklerSayisi { get; set; }
        public List<ToplulukOlusturmaIstegi> BekleyenIstekler { get; set; } = new List<ToplulukOlusturmaIstegi>();
    }
}