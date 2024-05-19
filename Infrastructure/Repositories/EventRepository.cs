using Infrastructure.DataModels;
using Infrastructure.DomainEntities;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContextEvent _dbContextEvent;

        private readonly EventMapper _eventMapper = new();

        public EventRepository(DbContextEvent dbContextEvent)
        {
            _dbContextEvent = dbContextEvent;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _dbContextEvent.Events.Select(eventObj => _eventMapper.MapEventEntityToModel(eventObj,))
                                               .ToListAsync();
        }

        public async Task<EventModel?> GetEventsById(int eventId)
        {
            return await _dbContextEvent.Events.Where(book => book.Id == eventId)
                                                .Select(eventObj => _eventMapper.MapEventModelToEntity(eventObj,))
                                                .FirstOrDefaultAsync();
        }

        public async Task<int> AddEvents(EventModel eventModel)
        {
            Event eventObj = _eventMapper.MapEventModelToEntity(eventModel);

            _dbContextEvent.Events.Add(eventObj);

            await _dbContextEvent.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task<int> UpdateEvents(int bookId, EventModel eventModel)
        {
            Event eventObj = _eventMapper.MapEventModelToEntity(eventModel);

            _dbContextEvent.Events.Update(eventObj);

            await _dbContextEvent.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task DeleteEvents(int eventId)
        {
            Event eventObj = new()
            {
                Id = eventId,
            };

            _dbContextEvent.Remove(eventObj);

            await _dbContextEvent.SaveChangesAsync();
        }
    }
}
