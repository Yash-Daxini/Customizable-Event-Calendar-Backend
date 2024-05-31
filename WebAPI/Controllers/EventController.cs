using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers;

[Route("api/users/{userId}/events")]
[ApiController]
//[Authorize]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;

    public EventController(IEventService eventService, IMapper mapper)
    {
        _eventService = eventService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllEvents([FromRoute] int userId)
    {
        try
        {
            List<Event> events = await _eventService.GetAllEventsByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("organizer-events")]
    public async Task<IActionResult> GetAllEventsCreatedByUser([FromRoute] int userId)
    {
        try
        {
            List<Event> events = await _eventService.GetAllEventCreatedByUser(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }


    [HttpGet("eventsBetweenDates")]
    public async Task<ActionResult> GetEventsWithInGivenDates([FromRoute] int userId, [FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
    {
        try
        {
            List<Event> events = await _eventService.GetEventsWithinGivenDatesByUserId(userId, startDate, endDate);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("proposed")]
    public async Task<ActionResult> GetProposedEvents([FromRoute] int userId)
    {
        try
        {
            List<Event> events = await _eventService.GetProposedEventsByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("daily")]
    public async Task<ActionResult> GetEventsForDailyView([FromRoute] int userId)
    {
        try
        {
            List<Event> events = await _eventService.GetEventsForDailyViewByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("weekly")]
    public async Task<ActionResult> GetEventsForWeeklyView([FromRoute] int userId)
    {
        try
        {
            List<Event> events = await _eventService.GetEventsForWeeklyViewByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("monthly")]
    public async Task<ActionResult> GetEventsForMonthlyView([FromRoute] int userId)
    {
        try
        {
            List<Event> events = await _eventService.GetEventsForMonthlyViewByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("~/api/events/{eventId}")]
    public async Task<ActionResult> GetEventById([FromRoute] int eventId, [FromRoute] int userId)
    {
        try
        {
            Event? eventModel = await _eventService.GetEventById(eventId, userId);

            if (eventModel is null) return NotFound();

            return Ok(_mapper.Map<EventResponseDto>(eventModel));
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("recurring-events")]
    public async Task<ActionResult> AddRecurringEvent([FromRoute] int userId, [FromBody] RecurringEventRequestDto recurringEventRequestDto)
    {
        try
        {
            Event eventObj = _mapper.Map<Event>(recurringEventRequestDto);

            int addedEventId = await _eventService.AddEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, new { addedEventId });
        }
        catch (EventOverlapException ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message});
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("")]
    public async Task<ActionResult> AddNonRecurringEvent([FromRoute] int userId, [FromBody] NonRecurringEventRequestDto nonRecurringEventRequestDto)
    {
        try
        {
            Event eventObj = _mapper.Map<Event>(nonRecurringEventRequestDto);

            int addedEventId = await _eventService.AddNonRecurringEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, new { addedEventId });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPut("{eventId}")]
    public async Task<ActionResult> UpdateEvent([FromRoute] int userId, [FromBody] RecurringEventRequestDto eventRequestDto)
    {
        try
        {
            Event eventObj = _mapper.Map<Event>(eventRequestDto);
            await _eventService.UpdateEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = eventObj.Id, controller = "event" }, new { eventObj.Id });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }

    }

    [HttpDelete("{eventId}")]
    public async Task<ActionResult> DeleteEvent([FromRoute] int eventId, [FromRoute] int userId)
    {
        try
        {
            await _eventService.DeleteEvent(eventId, userId);
            return Ok();
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("~/api/sharedCalendars/{sharedCalendarId}/events")]
    public async Task<ActionResult> GetSharedEventsFromSharedCalendarId([FromRoute] int sharedCalendarId)
    {
        try
        {
            List<Event> events = await _eventService.GetSharedEvents(sharedCalendarId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

}
