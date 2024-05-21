using Core.Domain;

namespace Core.Interfaces;

public interface IEventService
{
    public Task<List<EventModel>> GetAllEvents();

    public Task<EventModel?> GetEventById(int eventId);

    public Task<int> AddEvent(EventModel eventModel);

    public Task<int> UpdateEvent(int eventId, EventModel eventModel);

    public Task DeleteEvent(int eventId);
}
