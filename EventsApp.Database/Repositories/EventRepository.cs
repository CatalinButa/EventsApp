using EventsApp.Database.Context;
using EventsApp.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventsApp.Database.Repositories
{
    public class EventRepository
    {
        public AppDbContext context { get; set; }

        public EventRepository(AppDbContext context)
        {
            this.context = context;
        }

        public List<Event> GetEvents()
        {
            List<Event> events = context.Events
            .Include(e => e.Owner)
            .Include(e => e.Participants)
            .Where(e => e.DeletedDate == null)
            .OrderBy(e => e.CreatedDate)
            .ToList();
            return events;
        }

        public Event GetEventById(int eventId)
        {
            Event _event = context.Events
            .Include(e => e.Owner)
            .Include(e => e.Participants)
            .Where(e => e.DeletedDate == null)
            .Where(e => e.EventId == eventId)
            .FirstOrDefault();
            if (_event == null)
            {
                throw new Exception("Event not found");
            }
            return _event;
        }

        public Event SaveEvent(Event newEvent)
        {
            newEvent.CreatedDate = DateTime.Now;
            context.Events.Add(newEvent);
            context.SaveChanges();
            return newEvent;
        }

        public Event UpdateEventById(Event finalEvent, int eventId)
        {
            Event eventToUpdate = GetEventById(eventId);
            eventToUpdate.Title = finalEvent.Title;
            eventToUpdate.Description = finalEvent.Description;
            eventToUpdate.Location = finalEvent.Location;
            eventToUpdate.StartDate = finalEvent.StartDate;
            eventToUpdate.EndDate = finalEvent.EndDate;
            eventToUpdate.Price = finalEvent.Price;
            eventToUpdate.EventType = finalEvent.EventType;
            eventToUpdate.Owner = finalEvent.Owner;
            eventToUpdate.Participants = finalEvent.Participants;
            eventToUpdate.UpdatedDate = DateTime.Now;
            context.SaveChanges();
            return eventToUpdate;
        }

        public Event DeleteEventById(int eventId)
        {
            Event eventToDelete = GetEventById(eventId);
            eventToDelete.DeletedDate = DateTime.Now;
            context.SaveChanges();
            return eventToDelete;
        }
    }
}
