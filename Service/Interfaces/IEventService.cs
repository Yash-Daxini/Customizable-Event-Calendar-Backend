using Core.Domain;

namespace Core.Interfaces;

public interface IEventService
{
    public Task<List<Event>> GetAllEvents();

    public Task<Event?> GetEventById(int eventId);

    public Task<int> AddEvent(Event eventModel);

    public Task<int> UpdateEvent(int eventId, Event eventModel);

    public Task DeleteEvent(int eventId);

    public Task<List<Event>> GetEventsWithinGivenDates(DateOnly startDate, DateOnly endDate);

    public Task<List<Event>> GetProposedEvents();
}
