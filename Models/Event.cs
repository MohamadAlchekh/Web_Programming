namespace FinalProject.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public DateTime Date { get; set; }
        public string Location { get; set; } = "";
        public string Type { get; set; } = ""; // "Online" veya "InPerson"
        public int ParticipantCount { get; set; }
        public string ImageUrl { get; set; } = "";
    }
} 