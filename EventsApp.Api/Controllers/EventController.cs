using EventsApp.Core.Services;
using EventsApp.Database.Dtos.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SoundSphere.Api.Controllers;

namespace EventsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EventController : BaseController
    {
        public EventService eventService { get; set; }

        public EventController(EventService eventService)
        {
            this.eventService = eventService;
        }

        [HttpGet]
        [Route("get-events")]
        public IActionResult GetEvents()
        {
            List<EventDto> eventDtos = eventService.GetEvents();
            return Ok(eventDtos);
        }

        [HttpGet]
        [Route("get-event/{eventId}")]
        public IActionResult GetEventById(int eventId)
        {
            EventDto eventDto = eventService.GetEventById(eventId);
            return Ok(eventDto);
        }

        [HttpPost]
        [Route("save-event")]
        public IActionResult SaveEvent(EventDto newEventDto)
        {
            int loggedInUserId = GetLoggedInUserId();
            EventDto savedEventDto = eventService.SaveEvent(newEventDto, loggedInUserId);
            return Ok(savedEventDto);
        }

        [HttpPut]
        [Route("update-event/{eventId}")]
        public IActionResult UpdateEventById(EventDto finalEventDto, int eventId)
        {
            int loggedInUserId = GetLoggedInUserId();
            EventDto updatedEventDto = eventService.UpdateEventById(finalEventDto, eventId, loggedInUserId);
            return Ok(updatedEventDto);
        }

        [HttpDelete]
        [Route("delete-event/{eventId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteEventById(int eventId)
        {
            EventDto deletedEventDto = eventService.DeleteEventById(eventId);
            return Ok(deletedEventDto);
        }

        [HttpPut]
        [Route("add-participant")]
        public IActionResult AddParticipantToEvent(int eventId, int userId)
        {
            int loggedInUserId = GetLoggedInUserId();
            EventDto updatedEventDto = eventService.AddParticipantToEvent(eventId, userId, loggedInUserId);
            return Ok(updatedEventDto);
        }

        [HttpPut]
        [Route("remove-participant")]
        public IActionResult RemoveParticipantFromEvent(int eventId, int userId)
        {
            int loggedInUserId = GetLoggedInUserId();
            EventDto updatedEventDto = eventService.RemoveParticipantFromEvent(eventId, userId, loggedInUserId);
            return Ok(updatedEventDto);
        }
    }
}
