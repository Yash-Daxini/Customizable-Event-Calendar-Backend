using AutoMapper;
using Core.Domain;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/sharedCalendars")]
    [ApiController]
    public class SharedCalendarController : ControllerBase
    {
        private readonly ISharedCalendarService _sharedCalendarService;
        private readonly IMapper _mapper;

        public SharedCalendarController(ISharedCalendarService sharedCalendarService, IMapper mapper)
        {
            _sharedCalendarService = sharedCalendarService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSharedCalendars()
        {
            try
            {
                List<SharedCalendar> sharedCalendars = await _sharedCalendarService.GetAllSharedCalendars();

                return Ok(_mapper.Map<List<SharedCalendarDto>>(sharedCalendars));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{sharedCalendarId}")]
        public async Task<ActionResult> GetSharedCalendarById([FromRoute] int sharedCalendarId)
        {
            try
            {
                SharedCalendar? sharedCalendarModel = await _sharedCalendarService.GetSharedCalendarById(sharedCalendarId);

                return Ok(_mapper.Map<SharedCalendarDto>(sharedCalendarModel));
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

        [HttpPost()]
        public async Task<ActionResult> AddUser([FromBody] SharedCalendarDto sharedCalendarDto)
        {
            try
            {
                SharedCalendar sharedCalendar = _mapper.Map<SharedCalendar>(sharedCalendarDto);

                int addedSharedCalendarId = await _sharedCalendarService.AddSharedCalendar(sharedCalendar);
                return CreatedAtAction(nameof(GetSharedCalendarById), new { sharedCalendarId = addedSharedCalendarId, controller = "sharedcalendar" }, addedSharedCalendarId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
