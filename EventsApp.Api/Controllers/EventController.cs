using EventsApp.Core.Services;
using EventsApp.Database.Entities;
using Microsoft.AspNetCore.Mvc;

namespace EventsApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
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
            List<Event> events = eventService.GetEvents();
            return Ok(events);
        }

        [HttpGet]
        [Route("get-event/{eventId}")]
        public IActionResult GetEventById(int userId)
        {
            Event _event = eventService.GetEventById(userId);
            return Ok(_event);
        }

        [HttpPost]
        [Route("save-event")]
        public IActionResult SaveEvent(Event newEvent)
        {
            Event savedEvent = eventService.SaveEvent(newEvent);
            return Ok(savedEvent);
        }

        [HttpPut]
        [Route("update-event/{eventId}")]
        public IActionResult UpdateEventById(Event finalEvent, int eventId)
        {
            Event updatedEvent = eventService.UpdateEventById(finalEvent, eventId);
            return Ok(updatedEvent);
        }

        [HttpDelete]
        [Route("delete-event/{eventId}")]
        public IActionResult DeleteEventById(int eventId)
        {
            Event deletedEvent = eventService.DeleteEventById(eventId);
            return Ok(deletedEvent);
        }
    }
}
