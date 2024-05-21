using Core.Domain;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SharedCalendarController : ControllerBase
    {
        private readonly ISharedCalendarService _sharedCalendarService;

        public SharedCalendarController(ISharedCalendarService sharedCalendarService)
        {
            _sharedCalendarService = sharedCalendarService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSharedCalendars()
        {
            return Ok(await _sharedCalendarService.GetAllSharedCalendars());
        }

        [HttpGet("{sharedCalendarId}")]
        public async Task<ActionResult> GetSharedCalendarById([FromRoute] int sharedCalendarId)
        {
            SharedCalendarModel? sharedCalendarModel = await _sharedCalendarService.GetSharedCalendarById(sharedCalendarId);

            if (sharedCalendarModel is null) return NotFound();

            return Ok(sharedCalendarModel);
        }

        [HttpPost()]
        public async Task<ActionResult> AddUser([FromBody] SharedCalendarModel sharedCalendarModel)
        {
            int addedSharedCalendarId = await _sharedCalendarService.AddSharedCalendar(sharedCalendarModel);
            return CreatedAtAction(nameof(GetSharedCalendarById), new { sharedCalendarId = addedSharedCalendarId, controller = "sharedcalendar" }, addedSharedCalendarId);
        }
    }
}
