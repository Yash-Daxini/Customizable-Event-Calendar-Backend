namespace WebAPI.Dtos;

public class DashboardDataDto
{
    public List<EventResponseDto> DailyEvents { get; set; }

    public List<EventResponseDto> WeeklyEvents { get; set; }

    public List<EventResponseDto> MonthlyEvents { get; set; }

    public List<EventResponseDto> OrganizedEvents { get; set; }

    public List<EventResponseDto> ProposedEvents { get; set; }

    public List<SharedCalendarDto> SharedCalendars { get; set; }
}
