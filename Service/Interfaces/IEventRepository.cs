using Core.Domain;

namespace Infrastructure.Repositories;

public interface IEventRepository
{
    public Task<List<EventModel>> GetAllEvents();

    public Task<EventModel?> GetEventsById(int eventId);

    public Task<int> AddEvent(EventModel eventModel);

    public Task<int> UpdateEvent(int eventId, EventModel eventModel);

    public Task DeleteEvent(int eventId);
}
