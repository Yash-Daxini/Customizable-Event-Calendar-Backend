using Core.Domain;

namespace Core.Interfaces.IRepositories;

public interface IEventRepository
{
    public Task<List<Event>> GetAllEventsByUserId(int userId);

    public Task<Event?> GetEventsById(int eventId);

    public Task<int> AddEvent(Event eventModel);

    public Task UpdateEvent(Event eventModel);

    public Task DeleteEvent(int eventId);

    public Task<List<Event>> GetEventsWithinGivenDateByUserId(int userId, DateOnly startDate, DateOnly endDate);

    public Task<List<Event>> GetProposedEventsByUserId(int userId);

    public Task<List<Event>> GetSharedEvents(SharedCalendar sharedCalendar);
}
