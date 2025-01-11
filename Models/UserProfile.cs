using System;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Username { get; set; } = "";
        public string Location { get; set; } = "";
        public DateTime JoinDate { get; set; }
        public int ActivityCount { get; set; }
        public int FollowersCount { get; set; }
        public int FollowingCount { get; set; }
        public List<Activity> Activities { get; set; } = new List<Activity>();
    }

    public class Activity
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public DateTime Date { get; set; }
    }
} 