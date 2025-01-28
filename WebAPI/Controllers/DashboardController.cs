using AutoMapper;
using Core.Entities;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.Dtos;

namespace WebAPI.Controllers
{
    [Route("api/dashboardData")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IEventService _eventService;
        private readonly ISharedCalendarService _sharedCalendarService;
        private readonly IMapper _mapper;
        public DashboardController(IEventService eventService,
                                   ISharedCalendarService sharedCalendarService,
                                   IMapper mapper)
        {
            _eventService = eventService;
            _sharedCalendarService = sharedCalendarService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllDashboardData()
        {
            try
            {
                int userId = int.Parse(HttpContext.Items["UserId"]?.ToString());

                List<Event> dailyEvents = await _eventService.GetEventsForDailyViewByUserId(userId);
                List<Event> weeklyEvents = await _eventService.GetEventsForWeeklyViewByUserId(userId);
                List<Event> monthlyEvents = await _eventService.GetEventsForMonthlyViewByUserId(userId);
                List<Event> organizedEvents = await _eventService.GetAllEventCreatedByUser(userId);
                List<Event> proposedEvents = await _eventService.GetProposedEventsByUserId(userId);

                List<SharedCalendar> sharedCalendars = await _sharedCalendarService
                                                         .GetAllSharedCalendars(userId);

                var response = new DashboardDataDto()
                {
                    DailyEvents = _mapper.Map<List<EventResponseDto>>(dailyEvents),
                    WeeklyEvents = _mapper.Map<List<EventResponseDto>>(weeklyEvents),
                    MonthlyEvents = _mapper.Map<List<EventResponseDto>>(monthlyEvents),
                    OrganizedEvents = _mapper.Map<List<EventResponseDto>>(organizedEvents),
                    ProposedEvents = _mapper.Map<List<EventResponseDto>>(proposedEvents),
                    SharedCalendars = _mapper.Map<List<SharedCalendarDto>>(sharedCalendars),
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ErrorMessage = ex.Message });
            }
        }
    }
}
