using Infrastructure.DataModels;
using Infrastructure.DomainEntities;

namespace Infrastructure.Repositories;

public interface IEventRepository
{
    public Task<List<Event>> GetAllEvents();

    public Task<EventModel?> GetEventsById(int eventId);

    public Task<int> AddEvents(EventModel eventModel);

    public Task<int> UpdateEvents(int bookId, EventModel eventModel);

    public Task DeleteEvents(int eventId);
}
