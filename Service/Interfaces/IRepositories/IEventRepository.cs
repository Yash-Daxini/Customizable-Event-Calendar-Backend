using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IEventRepository : IRepository<Event>
{
    public Task<List<Event>> GetAllEventsByUserId(int userId);

    public Task<Event?> GetEventById(int eventId);

    public Task<List<Event>> GetEventsWithinGivenDateByUserId(int userId,
                                                              DateOnly startDate, 
                                                              DateOnly endDate);
}
