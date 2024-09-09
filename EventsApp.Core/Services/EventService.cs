using EventsApp.Database.Entities;
using EventsApp.Database.Repositories;

namespace EventsApp.Core.Services
{
    public class EventService
    {
        public EventRepository eventRepository { get; set; }

        public EventService(EventRepository eventRepository)
        {
            this.eventRepository = eventRepository;
        }

        public List<Event> GetEvents()
        {
            List<Event> events = eventRepository.GetEvents();
            return events;
        }

        public Event GetEventById(int eventId)
        {
            Event _event = eventRepository.GetEventById(eventId);
            return _event;
        }

        public Event SaveEvent(Event newEvent)
        {
            Event savedEvent = eventRepository.SaveEvent(newEvent);
            return savedEvent;
        }

        public Event UpdateEventById(Event finalEvent, int eventId)
        {
            Event updatedEvent = eventRepository.UpdateEventById(finalEvent, eventId);
            return updatedEvent;
        }

        public Event DeleteEventById(int eventId)
        {
            Event deletedEvent = eventRepository.DeleteEventById(eventId);
            return deletedEvent;
        }
    }
}
