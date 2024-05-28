using AutoMapper;
using Core.Domain;
using Core.Interfaces.IRepositories;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EventRepository : IEventRepository
{
    private readonly DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public EventRepository(DbContextEventCalendar dbContextEvent, IMapper mapper)
    {
        _dbContext = dbContextEvent;
        _mapper = mapper;
    }

    public async Task<List<Event>> GetAllEventsByUserId(int userId)
    {
        List<EventDataModel> events = await _dbContext.Events
                                                      .Where(eventObj => eventObj.UserId == userId)
                                                      .Include(eventObj => eventObj.EventCollaborators)
                                                        .ThenInclude(eventCollaborator => eventCollaborator.User)
                                                      .ToListAsync();

        return _mapper.Map<List<Event>>(events);

    }

    public async Task<Event?> GetEventsById(int eventId)
    {
        return _mapper.Map<Event>
                (
                       await _dbContext
                      .Events
                      .Where(eventObj => eventObj.Id == eventId)
                      .Include(eventObj => eventObj.EventCollaborators)
                        .ThenInclude(eventCollaborator => eventCollaborator.User)
                      .FirstOrDefaultAsync()
                );
    }

    public async Task<int> AddEvent(Event eventModel)
    {
        EventDataModel eventObj = _mapper.Map<EventDataModel>(eventModel);

        _dbContext.Events.Add(eventObj);

        await _dbContext.SaveChangesAsync();

        return eventObj.Id;
    }

    public async Task UpdateEvent(Event eventModel)
    {
        EventDataModel eventObj = _mapper.Map<EventDataModel>(eventModel);

        _dbContext.Events.Update(eventObj);

        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteEvent(Event eventObj)
    {
        _dbContext.Remove(_mapper.Map<EventDataModel>(eventObj));

        await _dbContext.SaveChangesAsync();
    }

    public async Task<List<Event>> GetEventsWithinGivenDateByUserId(int userId, DateOnly startDate, DateOnly endDate)
    {
        List<EventDataModel> events = await _dbContext.Events
                                                      .Where(eventObj => eventObj.UserId == userId)
                                                      .Include(eventObj => eventObj.EventCollaborators
                                                      .Where(eventCollaborator => eventCollaborator.EventDate >= startDate
                                                             && eventCollaborator.EventDate <= endDate))
                                                        .ThenInclude(eventCollaborator => eventCollaborator.User)
                                                      .ToListAsync();

        return _mapper.Map<List<Event>>(events);
    }

    public async Task<List<Event>> GetProposedEventsByUserId(int userId)
    {
        List<EventDataModel> events = await _dbContext.Events
                                                      .Include(eventObj => eventObj.EventCollaborators)
                                                        .ThenInclude(eventCollaborator => eventCollaborator.User)
                                                      .Where(eventObj => eventObj
                                                                        .EventCollaborators
                                                                        .Select(eventCollaborator => eventCollaborator.UserId)
                                                                        .Contains(userId))
                                                      .ToListAsync();
        return _mapper.Map<List<Event>>(events);
    }

    public async Task<List<Event>> GetSharedEvents(SharedCalendar sharedCalendar)
    {
        List<Event> events = await GetEventsWithinGivenDateByUserId(sharedCalendar.SenderUser.Id, sharedCalendar.FromDate, sharedCalendar.ToDate);

        return events
               .Where(eventModel => eventModel.GetEventOrganizer().Id == sharedCalendar.SenderUser.Id)
               .ToList();
    }
}
