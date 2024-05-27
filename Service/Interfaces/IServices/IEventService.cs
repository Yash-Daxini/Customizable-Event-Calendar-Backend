using Core.Domain;

namespace Core.Interfaces.IServices;

public interface IEventService
{
    public Task<List<Event>> GetAllEvents();

    public Task<Event?> GetEventById(int eventId);

    public Task<int> AddEvent(Event eventModel);

    public Task<int> UpdateEvent(Event eventModel);

    public Task DeleteEvent(int eventId);

    public Task<List<Event>> GetEventsWithinGivenDates(DateOnly startDate, DateOnly endDate);

    public Task<List<Event>> GetProposedEvents();

    public Task<List<Event>> GetEventsByUserId(int userId);

    public Task<List<Event>> GetNonProposedEventsByUserId(int userId);

    public Task<List<Event>> GetEventsForDailyView();

    public Task<List<Event>> GetEventsForWeeklyView();

    public Task<List<Event>> GetEventsForMonthlyView();

    public Task<List<Event>> GetSharedEvents(int sharedCalendarId);
}
