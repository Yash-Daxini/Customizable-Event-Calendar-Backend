using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Filters;

namespace WebAPI.Controllers
{
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("~/api/events/{eventId}")]
        public async Task<ActionResult> GetEventById([FromRoute] int eventId)
        {
            try
            {
                Event? eventModel = await _eventService.GetEventById(eventId);

                if (eventModel is null) return NotFound();

                return Ok(_mapper.Map<EventResponseDto>(eventModel));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("~/api/events")]
        [ServiceFilter(typeof(ValidationFilter<EventRequestDto>))]
        public async Task<ActionResult> AddEvent([FromBody] EventRequestDto eventRequestDto) //Validation added for test
        {
            try
            {
                Event eventObj = _mapper.Map<Event>(eventRequestDto);

                int addedEventId = await _eventService.AddEvent(eventObj);
                return CreatedAtAction(nameof(GetEventById), new { eventId = addedEventId, controller = "event" }, addedEventId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("~/api/events")]
        [ServiceFilter(typeof(ValidationFilter<EventRequestDto>))]
        public async Task<ActionResult> UpdateEvent([FromBody] EventRequestDto eventRequestDto)
        {
            try
            {
                Event eventObj = _mapper.Map<Event>(eventRequestDto);
                await _eventService.UpdateEvent(eventObj);
                return CreatedAtAction(nameof(GetEventById), new { eventId = eventObj.Id, controller = "event" }, eventObj.Id);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }

        }

        [HttpDelete("~/api/events/{eventId}")]
        public async Task<ActionResult> DeleteEvent([FromRoute] int eventId)
        {
            try
            {
                await _eventService.DeleteEvent(eventId);
                return Ok();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
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
                return StatusCode(500, ex.Message);
            }
        }

    }
}
