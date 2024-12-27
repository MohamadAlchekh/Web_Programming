using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;
using System;
using System.Collections.Generic;

namespace FinalProject.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index(string username)
        {
            // Şimdilik örnek veri oluşturalım
            var userProfile = new UserProfile
            {
                Id = 1,
                Name = "Test Kullanıcı",
                Username = username ?? "testuser",
                Location = "İstanbul",
                JoinDate = DateTime.Now.AddMonths(-3),
                ActivityCount = 5,
                FollowersCount = 120,
                FollowingCount = 85,
                Activities = new List<Activity>
                {
                    new Activity 
                    { 
                        Id = 1, 
                        Name = "Yazılım Geliştirme Konferansı", 
                        Type = "Etkinlik", 
                        Date = DateTime.Now.AddDays(-5) 
                    },
                    new Activity 
                    { 
                        Id = 2, 
                        Name = "UX/UI Workshop", 
                        Type = "Workshop", 
                        Date = DateTime.Now.AddDays(-2) 
                    }
                }
            };

            return View(userProfile);
        }
    }
} 