using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;
using Infrastructure.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContextEventCalendar _dbContextEventCalendar;
        private readonly EventMapper _eventMapper;
        private readonly IMapper _mapper;

        public EventRepository(DbContextEventCalendar dbContextEvent, EventMapper eventMapper, IMapper mapper)
        {
            _dbContextEventCalendar = dbContextEvent;
            _eventMapper = eventMapper;
            _mapper = mapper;
        }

        public async Task<List<EventModel>> GetAllEvents()
        {
            return await _dbContextEventCalendar
                        .Events
                        .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                        .Select(eventObj => _mapper.Map<EventModel>(eventObj))
                                               .ToListAsync();

        }

        public async Task<EventModel?> GetEventsById(int eventId)
        {
            return await _dbContextEventCalendar
                          .Events
                          .Where(book => book.Id == eventId)
                          .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                          .Select(eventObj => _mapper.Map<EventModel>(eventObj))
                                                .FirstOrDefaultAsync();
        }

        public async Task<int> AddEvent(EventModel eventModel)
        {
            Event eventObj = _eventMapper.MapEventModelToEntity(eventModel);

            _dbContextEventCalendar.Events.Add(eventObj);

            await _dbContextEventCalendar.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task<int> UpdateEvent(int eventId, EventModel eventModel)
        {
            Event eventObj = _eventMapper.MapEventModelToEntity(eventModel);

            eventObj.Id = eventId;

            _dbContextEventCalendar.Events.Update(eventObj);

            await _dbContextEventCalendar.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task DeleteEvent(int eventId)
        {
            Event eventObj = new()
            {
                Id = eventId,
            };

            _dbContextEventCalendar.Remove(eventObj);

            await _dbContextEventCalendar.SaveChangesAsync();
        }
    }
}
