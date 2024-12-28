namespace FinalProject.Models
{
    public class Community
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string University { get; set; } = "";
        public int MemberCount { get; set; }
        public string LogoUrl { get; set; } = "";
        public bool IsVerified { get; set; }
    }
} 