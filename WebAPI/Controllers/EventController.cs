using AutoMapper;
using Core.Domain;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/events")]
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
        public async Task<IActionResult> GetAllEvents()
        {
            List<Event> events = await _eventService.GetAllEvents();

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult> GetEventById([FromRoute] int eventId)
        {
            Event? eventModel = await _eventService.GetEventById(eventId);

            if (eventModel is null) return NotFound();

            return Ok(_mapper.Map<EventResponseDto>(eventModel));
        }

        [HttpPost("")]
        public async Task<ActionResult> AddEvent([FromBody] EventRequestDto eventRequestDto)
        {
            try
            {
                Event eventObj = _mapper.Map<Event>(eventRequestDto);

                int addedEventId = await _eventService.AddEvent(eventObj);
                return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, addedEventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{eventId}")]
        public async Task<ActionResult> UpdateEvent([FromRoute] int eventId, [FromBody] EventRequestDto eventRequestDto)
        {
            try
            {
                Event eventObj = _mapper.Map<Event>(eventRequestDto);
                eventObj.Id = eventId;
                int addedEventId = await _eventService.UpdateEvent(eventObj);
                return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, addedEventId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{eventId}")]
        public async Task<ActionResult> DeleteUser([FromRoute] int eventId)
        {
            try
            {
                await _eventService.DeleteEvent(eventId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("eventsBetweenDates")]
        public async Task<ActionResult> GetEventsWithInGivenDates([FromQuery] DateOnly startDate, [FromQuery] DateOnly endDate)
        {
            List<Event> events = await _eventService.GetEventsWithinGivenDates(startDate, endDate);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("proposed")]
        public async Task<ActionResult> GetProposedEvents()
        {
            List<Event> events = await _eventService.GetProposedEvents();

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("~/api/users/{userId}/events")]
        public async Task<ActionResult> GetEventsByUser([FromRoute] int userId)
        {
            List<Event> events = await _eventService.GetEventsByUserId(userId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("daily")]
        public async Task<ActionResult> GetEventsForDailyView()
        {
            List<Event> events = await _eventService.GetEventsForDailyView();

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("weekly")]
        public async Task<ActionResult> GetEventsForWeeklyView()
        {
            List<Event> events = await _eventService.GetEventsForWeeklyView();

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("monthly")]
        public async Task<ActionResult> GetEventsForMonthlyView()
        {
            List<Event> events = await _eventService.GetEventsForMonthlyView();

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

        [HttpGet("~/api/sharedCalendars/{sharedCalendarId}/events")]
        public async Task<ActionResult> GetSharedEventsFromSharedCalendarId([FromRoute] int sharedCalendarId)
        {
            List<Event> events = await _eventService.GetSharedEvents(sharedCalendarId);

            return Ok(_mapper.Map<List<EventResponseDto>>(events));
        }

    }
}
