using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IEventService
{
    public Task<List<Event>> GetAllEventsByUserId(int userId);

    public Task<Event> GetEventById(int eventId, int userId);

    public Task<int> AddEvent(Event eventModel, int userId);

    public Task<int> AddNonRecurringEvent(Event eventModel, int userId);

    public Task UpdateEvent(Event eventModel, int userId);

    public Task DeleteEvent(int eventId, int userId);

    public Task<List<Event>> GetEventsWithinGivenDatesByUserId(int userId, DateOnly startDate, DateOnly endDate);

    public Task<List<Event>> GetProposedEventsByUserId(int userId);

    public Task<List<Event>> GetNonProposedEventsByUserId(int userId);

    public Task<List<Event>> GetEventsForDailyViewByUserId(int userId);

    public Task<List<Event>> GetEventsForWeeklyViewByUserId(int userId);

    public Task<List<Event>> GetEventsForMonthlyViewByUserId(int userId);

    public Task<List<Event>> GetSharedEvents(int sharedCalendarId);
}
