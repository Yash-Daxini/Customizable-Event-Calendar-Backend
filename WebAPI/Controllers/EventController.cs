using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers;

[Route("api/events")]
[ApiController]
[Authorize]
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
    public async Task<IActionResult> GetAllEvents()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            List<Event> events = await _eventService.GetAllEventsByUserId(userId);

            var e = _mapper.Map<List<EventResponseDto>>(events);

            return Ok(e);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("organizer-events")]
    public async Task<IActionResult> GetAllEventsCreatedByUser()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            List<Event> events = await _eventService.GetAllEventCreatedByUser(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }


    [HttpGet("eventsBetweenDates")]
    public async Task<ActionResult> GetEventsWithInGivenDates([FromQuery] DateOnly startDate,
                                                              [FromQuery] DateOnly endDate)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            List<Event> events = await _eventService.GetEventsWithinGivenDatesByUserId(userId, startDate, endDate);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("proposed")]
    public async Task<ActionResult> GetProposedEvents()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            List<Event> events = await _eventService.GetProposedEventsByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("daily")]
    public async Task<ActionResult> GetEventsForDailyView()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

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
    public async Task<ActionResult> GetEventsForMonthlyView()
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            List<Event> events = await _eventService.GetEventsForMonthlyViewByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpGet("~/api/events/{eventId}")]
    public async Task<ActionResult> GetEventById([FromRoute] int eventId)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            Event? eventModel = await _eventService.GetEventById(eventId, userId);

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
    public async Task<ActionResult> AddRecurringEvent([FromBody] RecurringEventRequestDto recurringEventRequestDto)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            Event eventObj = _mapper.Map<Event>(recurringEventRequestDto);

            int addedEventId = await _eventService.AddEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, new { addedEventId });
        }
        catch (EventOverlapException ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPost("")]
    public async Task<ActionResult> AddNonRecurringEvent([FromBody] NonRecurringEventRequestDto nonRecurringEventRequestDto)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            Event eventObj = _mapper.Map<Event>(nonRecurringEventRequestDto);

            int addedEventId = await _eventService.AddNonRecurringEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, new { addedEventId });
        }
        catch (EventOverlapException ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }
    }

    [HttpPut("{eventId}")]
    public async Task<ActionResult> UpdateNonRecurringEvent([FromBody] NonRecurringEventRequestDto eventRequestDto)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            Event eventObj = _mapper.Map<Event>(eventRequestDto);
            await _eventService.UpdateEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = eventObj.Id, controller = "event" }, new { eventObj.Id });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (EventOverlapException ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }

    }

    [HttpPut("recurring-events/{eventId}")]
    public async Task<ActionResult> UpdateRecurringEvent([FromBody] RecurringEventRequestDto eventRequestDto)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            Event eventObj = _mapper.Map<Event>(eventRequestDto);
            await _eventService.UpdateEvent(eventObj, userId);
            return CreatedAtAction(nameof(GetEventById), new { eventId = eventObj.Id, controller = "event" }, new { eventObj.Id });
        }
        catch (NotFoundException ex)
        {
            return NotFound(new { ErrorMessage = ex.Message });
        }
        catch (EventOverlapException ex)
        {
            return BadRequest(new { ErrorMessage = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { ErrorMessage = ex.Message });
        }

    }

    [HttpDelete("{eventId}")]
    public async Task<ActionResult> DeleteEvent([FromRoute] int eventId)
    {
        try
        {
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

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
            //TODO:- Get only given user's shared calendars
            int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

            List<Event> events = await _eventService.GetSharedEvents(sharedCalendarId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
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

}
