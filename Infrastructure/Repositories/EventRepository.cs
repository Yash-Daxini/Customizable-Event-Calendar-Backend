using AutoMapper;
using Core.Domain;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContextEventCalendar _dbContextEventCalendar;

        private readonly IMapper _mapper;

        public EventRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContextEventCalendar = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _dbContextEventCalendar
                        .Events
                        .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                        .Select(eventObj => _mapper.Map<Event>(eventObj))
                                               .ToListAsync();

        }

        public async Task<Event?> GetEventsById(int eventId)
        {
            return await _dbContextEventCalendar
                          .Events
                          .Where(book => book.Id == eventId)
                          .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                          .Select(eventObj => _mapper.Map<Event>(eventObj))
                                                .FirstOrDefaultAsync();
        }

        public async Task<int> AddEvent(Event eventModel)
        {
            EventDataModel eventObj = _mapper.Map<EventDataModel>(eventModel);

            _dbContextEventCalendar.Events.Add(eventObj);

            await _dbContextEventCalendar.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task<int> UpdateEvent(int eventId, Event eventModel)
        {
            EventDataModel eventObj = _mapper.Map<EventDataModel>(eventModel);

            eventObj.Id = eventId;

            _dbContextEventCalendar.Events.Update(eventObj);

            await _dbContextEventCalendar.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task DeleteEvent(int eventId)
        {
            EventDataModel eventObj = new()
            {
                Id = eventId,
            };

            _dbContextEventCalendar.Remove(eventObj);

            await _dbContextEventCalendar.SaveChangesAsync();
        }

        public async Task<List<Event>> GetEventsWithinGivenDate(DateOnly startDate, DateOnly endDate)
        {
            return [.._dbContextEventCalendar
                   .Events
                   .Where(eventObj => eventObj
                                      .EventCollaborators
                                      .Where(eventCollaborator=>eventCollaborator.EventDate >= startDate && eventCollaborator.EventDate <= endDate)
                                      .Select(eventCollaborator=>eventCollaborator.EventId).Contains(eventObj.Id))
                   .Select(eventObj => _mapper.Map<Event>(eventObj))];
        }
    }
}
