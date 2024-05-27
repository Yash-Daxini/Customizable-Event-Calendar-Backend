using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Domain;
using Core.Interfaces.IRepositories;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly DbContextEventCalendar _dbContext;

        private readonly IMapper _mapper;

        public EventRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
        {
            _dbContext = dbContextEvent;
            _mapper = mapper;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return
                        await _dbContext
                        .Events
                        .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                            .Select(eventObj => _mapper.Map<Event>(eventObj))
                        .ToListAsync();

        }

        public async Task<Event?> GetEventsById(int eventId)
        {
            return _mapper.Map<Event>(
                           await _dbContext
                          .Events
                          .Where(book => book.Id == eventId)
                          .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                          .FirstOrDefaultAsync());
        }

        public async Task<int> AddEvent(Event eventModel)
        {
            EventDataModel eventObj = _mapper.Map<EventDataModel>(eventModel);

            foreach (var eventCollaborator in eventObj.EventCollaborators)
            {
                _dbContext.Attach(eventCollaborator.User);
            }

            _dbContext.Events.Add(eventObj);

            await _dbContext.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task<int> UpdateEvent(int eventId, Event eventModel)
        {
            EventDataModel eventObj = _mapper.Map<EventDataModel>(eventModel);

            eventObj.Id = eventId;

            _dbContext.Events.Update(eventObj);

            await _dbContext.SaveChangesAsync();

            return eventObj.Id;
        }

        public async Task DeleteEvent(int eventId)
        {
            EventDataModel eventObj = new()
            {
                Id = eventId,
            };

            _dbContext.Remove(eventObj);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Event>> GetEventsWithinGivenDate(DateOnly startDate, DateOnly endDate)
        {
            return await _dbContext
                          .Events
                          .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                          .Where(eventObj => eventObj
                                              .EventCollaborators
                                              .Where(eventCollaborator => eventCollaborator.EventDate >= startDate && eventCollaborator.EventDate <= endDate)
                                              .Select(eventCollaborator => eventCollaborator.EventId).Contains(eventObj.Id))
                          .Select(eventObj => _mapper.Map<Event>(eventObj))
                          .ToListAsync();
        }

        public async Task<List<Event>> GetProposedEvents()
        {
            return _mapper.Map<List<Event>>(
                           await _dbContext
                          .Events
                          .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                          .ToListAsync());
        }

        public async Task<List<Event>> GetEventsByUserId(int userId)
        {
            return _mapper.Map<List<Event>>(
                           await _dbContext
                          .Events
                          .Where(eventObj => eventObj.UserId == userId)
                          .Include(eventObj => eventObj.EventCollaborators)
                            .ThenInclude(eventCollaborator => eventCollaborator.User)
                          .ToListAsync());
        }

        public async Task<List<Event>> GetSharedEventsFromSharedCalendarId(SharedCalendar? sharedCalendar)
        {
            if (sharedCalendar is null) return [];

            return await GetSharedEvents(sharedCalendar);
        }

        public async Task<List<Event>> GetSharedEvents(SharedCalendar sharedCalendar)
        {
            List<Event> events = await GetEventsWithinGivenDate(sharedCalendar.FromDate, sharedCalendar.ToDate);

            return events
                   .Where(eventModel => eventModel.GetEventOrganizer().Id == sharedCalendar.SenderUser.Id)
                   .ToList();
        }
    }
}
