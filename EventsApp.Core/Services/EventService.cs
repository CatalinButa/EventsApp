﻿using EventsApp.Database.Context;
using EventsApp.Database.Dtos.Common;
using EventsApp.Database.Entities;
using EventsApp.Database.Repositories;

namespace EventsApp.Core.Services
{
    public class EventService
    {
        public EventRepository eventRepository { get; set; }
        public UserRepository userRepository { get; set; }
        public AppDbContext context { get; set; }

        public EventService(EventRepository eventRepository, UserRepository userRepository, AppDbContext context)
        {
            this.eventRepository = eventRepository;
            this.userRepository = userRepository;
            this.context = context;
        }

        public List<EventDto> GetEvents()
        {
            List<Event> events = eventRepository.GetEvents();
            List<EventDto> eventDtos = new List<EventDto>();
            foreach (Event _event in events)
            {
                EventDto eventDto = ConvertToEventDto(_event);
                eventDtos.Add(eventDto);
            }
            return eventDtos;
        }

        public EventDto GetEventById(int eventId)
        {
            Event _event = eventRepository.GetEventById(eventId);
            EventDto eventDto = ConvertToEventDto(_event);
            return eventDto;
        }

        public EventDto SaveEvent(EventDto newEventDto, int loggedInUserId)
        {
            Event newEvent = ConvertToEvent(newEventDto);
            Event savedEvent = eventRepository.SaveEvent(newEvent);
            EventDto savedEventDto = ConvertToEventDto(savedEvent);
            savedEventDto.OwnerId = loggedInUserId;
            return savedEventDto;
        }

        public EventDto UpdateEventById(EventDto finalEventDto, int eventId, int loggedInUserId)
        {
            Event eventToUpdate = eventRepository.GetEventById(eventId);
            if (eventToUpdate.Owner.UserId != loggedInUserId)
            {
                throw new Exception("You can't update the details of another user's event");
            }
            Event finalEvent = ConvertToEvent(finalEventDto);
            Event updatedEvent = eventRepository.UpdateEventById(finalEvent, eventId);
            EventDto updatedEventDto = ConvertToEventDto(updatedEvent);
            return updatedEventDto;
        }

        public EventDto DeleteEventById(int eventId)
        {
            Event deletedEvent = eventRepository.DeleteEventById(eventId);
            EventDto deletedEventDto = ConvertToEventDto(deletedEvent);
            return deletedEventDto;
        }

        public EventDto AddParticipantToEvent(int eventId, int userId, int loggedInUserId)
        {
            Event _event = eventRepository.GetEventById(eventId);
            if (_event.Owner.UserId != loggedInUserId)
            {
                throw new Exception("You can't update the list of participants of another user's event");
            }
            if (_event.Owner.UserId == userId)
            {
                throw new Exception("You (the owner) can't add yourself as a participant");
            }
            UserEvent participant = new UserEvent();
            participant.UserId = userId;
            participant.EventId = eventId;
            _event.Participants.Add(participant);
            context.SaveChanges();
            EventDto eventDto = ConvertToEventDto(_event);
            return eventDto;
        }

        public EventDto RemoveParticipantFromEvent(int eventId, int userId, int loggedInUserId)
        {
            Event _event = eventRepository.GetEventById(eventId);
            if (_event.Owner.UserId != loggedInUserId)
            {
                throw new Exception("You can't update the list of participants of another user's event");
            }
            if (_event.Owner.UserId == userId)
            {
                throw new Exception("You (the owner) can't remove yourself from the list of participants");
            }
            UserEvent participant = _event.Participants.FirstOrDefault(p => p.UserId == userId);
            if (participant == null)
            {
                throw new Exception("This user doesn't participate in the event");
            }
            _event.Participants.Remove(participant);
            context.SaveChanges();
            EventDto eventDto = ConvertToEventDto(_event);
            return eventDto;
        }

        private EventDto ConvertToEventDto(Event _event)
        {
            EventDto eventDto = new EventDto();
            eventDto.EventId = _event.EventId;
            eventDto.Title = _event.Title;
            eventDto.Description = _event.Description;
            eventDto.Location = _event.Location;
            eventDto.StartDate = _event.StartDate;
            eventDto.EndDate = _event.EndDate;
            eventDto.Price = _event.Price;
            eventDto.EventType = _event.EventType;
            eventDto.OwnerId = _event.Owner.UserId;
            eventDto.ParticipantsIds = new List<int>();
            foreach (UserEvent participant in _event.Participants)
            {
                eventDto.ParticipantsIds.Add(participant.UserId);
            }
            eventDto.CreatedDate = _event.CreatedDate;
            eventDto.UpdatedDate = _event.UpdatedDate;
            eventDto.DeletedDate = _event.DeletedDate;
            return eventDto;
        }

        private Event ConvertToEvent(EventDto eventDto)
        {
            Event _event = new Event();
            _event.EventId = eventDto.EventId;
            _event.Title = eventDto.Title;
            _event.Description = eventDto.Description;
            _event.Location = eventDto.Location;
            _event.StartDate = eventDto.StartDate;
            _event.EndDate = eventDto.EndDate;
            _event.Price = eventDto.Price;
            _event.EventType = eventDto.EventType;
            _event.Owner = userRepository.GetUserById(eventDto.OwnerId);
            _event.Participants = new List<UserEvent>();
            foreach (int participantId in eventDto.ParticipantsIds)
            {
                User user = userRepository.GetUserById(participantId);
                UserEvent participant = new UserEvent();
                participant.User = user;
                participant.Event = _event;
                _event.Participants.Add(participant);
            }
            _event.CreatedDate = eventDto.CreatedDate;
            _event.UpdatedDate = eventDto.UpdatedDate;
            _event.DeletedDate = eventDto.DeletedDate;
            return _event;
        }
    }
}
