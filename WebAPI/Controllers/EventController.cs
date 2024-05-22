﻿using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEvents()
        {
            return Ok(await _eventService.GetAllEvents());
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult> GetEventById([FromRoute] int eventId)
        {
            Event? eventModel = await _eventService.GetEventById(eventId);

            if (eventModel is null) return NotFound();

            return Ok(eventModel);
        }

        [HttpPost("")]
        public async Task<ActionResult> AddEvent([FromBody] Event eventModel)
        {
            int addedEventId = await _eventService.AddEvent(eventModel);
            return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, addedEventId);
        }

        [HttpPut("{eventId}")]
        public async Task<ActionResult> UpdateEvent([FromRoute] int eventId, [FromBody] Event eventModel)
        {
            int addedEventId = await _eventService.UpdateEvent(eventId, eventModel);
            return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, addedEventId);
        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int eventId)
        {
            await _eventService.DeleteEvent(eventId);
            return Ok();
        }

        [HttpGet("/eventsByDate")]
        public async Task<ActionResult> GetEventsWithInGivenDates([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            return Ok(await _eventService.GetEventsWithinGivenDates(startDate, endDate));
        }
        
        [HttpGet("/proposedEvents")]
        public async Task<ActionResult> GetProposedEvents()
        {
            return Ok(await _eventService.GetProposedEvents());
        }

    }
}
