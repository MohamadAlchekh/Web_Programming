using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers
{
    public class CommunitiesController : Controller
    {
        public IActionResult Index()
        {
            var communities = new List<Community>
            {
                new Community
                {
                    Id = 1,
                    Name = "UÜ Bilişim Topluluğu",
                    Description = "Yazılım, donanım ve teknoloji alanlarında etkinlikler düzenleyen öğrenci topluluğu",
                    University = "Uludağ Üniversitesi",
                    MemberCount = 280,
                    IsVerified = true
                },
                new Community
                {
                    Id = 2,
                    Name = "Yapay Zeka Kulübü",
                    Description = "AI ve makine öğrenmesi alanında çalışmalar yapan öğrenci topluluğu",
                    University = "Uludağ Üniversitesi",
                    MemberCount = 180,
                    IsVerified = true
                },
                new Community
                {
                    Id = 3,
                    Name = "Siber Güvenlik Topluluğu",
                    Description = "Siber güvenlik ve etik hacking konularında çalışmalar yapan topluluk",
                    University = "Uludağ Üniversitesi",
                    MemberCount = 150,
                    IsVerified = true
                }
                // Daha fazla topluluk eklenebilir
            };

            return View(communities);
        }
    }
} 