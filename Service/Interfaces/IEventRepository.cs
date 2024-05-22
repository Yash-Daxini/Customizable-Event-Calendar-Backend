using Core.Domain;

namespace Infrastructure.Repositories;

public interface IEventRepository
{
    public Task<List<Event>> GetAllEvents();

    public Task<Event?> GetEventsById(int eventId);

    public Task<int> AddEvent(Event eventModel);

    public Task<int> UpdateEvent(int eventId, Event eventModel);

    public Task DeleteEvent(int eventId);

    public Task<List<Event>> GetEventsWithinGivenDate(DateOnly startDate, DateOnly endDate);
}
