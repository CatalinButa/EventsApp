using EventsApp.Database.Enums;

namespace EventsApp.Database.Entities
{
    public class Event : BaseEntity
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public EventType EventType { get; set; }
        public User Owner { get; set; }
        public List<UserEvent> Participants { get; set; } = new List<UserEvent>();
    }
}
