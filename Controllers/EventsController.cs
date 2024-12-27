using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class EventsController : Controller
    {
        public IActionResult Index()
        {
            var events = new List<Event>
            {
                new Event
                {
                    Id = 1,
                    Title = "Yazılım Geliştirme Konferansı",
                    Description = "Modern web teknolojileri ve best practice'ler hakkında detaylı bilgi edinin.",
                    Date = DateTime.Now.AddDays(5),
                    Location = "Online",
                    Type = "Online",
                    ParticipantCount = 150
                },
                new Event
                {
                    Id = 2,
                    Title = "UX/UI Workshop",
                    Description = "Kullanıcı deneyimi tasarımı hakkında pratik bilgiler.",
                    Date = DateTime.Now.AddDays(10),
                    Location = "İstanbul",
                    Type = "InPerson",
                    ParticipantCount = 30
                },
                new Event
                {
                    Id = 3,
                    Title = "Yapay Zeka Semineri",
                    Description = "AI ve Machine Learning alanındaki son gelişmeler.",
                    Date = DateTime.Now.AddDays(15),
                    Location = "Ankara",
                    Type = "InPerson",
                    ParticipantCount = 75
                },
                new Event
                {
                    Id = 4,
                    Title = "Unity ile Oyun Atölyesi",
                    Description = "C# kullanılarak yapılacak workshop.",
                    Date = DateTime.Now.AddDays(20),
                    Location = "Online",
                    Type = "Online",
                    ParticipantCount =50
                },
                new Event
                {
                    Id = 5,
                    Title = "CyberSecurity BootCamp",
                    Description = "Ağ sızma eğitimi ve artısı verilecek o eğitim serisi ",
                    Date = DateTime.Now.AddDays(10),
                    Location = "Bursa",
                    Type = "InPerson",
                    ParticipantCount = 150
                },
                new Event
                {
                    Id = 6,
                    Title = "Photoshop ve Indesign Hakkında temel bilgiler.",
                    Description = "AI ve Machine Learning alanındaki son gelişmeler.",
                    Date = DateTime.Now.AddDays(18),
                    Location = "Online",
                    Type = "Online",
                    ParticipantCount = 50
                }
            };

            return View(events);
        }
    }
} 