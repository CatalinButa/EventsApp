using EventsApp.Database.Entities;
using EventsApp.Database.Enums;

namespace EventsApp.Database.Dtos.Common
{
    public class EventDto : BaseEntity
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public EventType EventType { get; set; }
        public int OwnerId { get; set; } = 1;
        public List<int> ParticipantsIds { get; set; } = new List<int>();
    }
}
