using AutoMapper;
using Core.Entities;
using Core.Interfaces.IRepositories;
using Infrastructure.DataModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EventRepository : BaseRepository<Event, EventDataModel>, IEventRepository
{
    private readonly DbContextEventCalendar _dbContext;

    private readonly IMapper _mapper;

    public EventRepository(DbContextEventCalendar dbContextEvent, IMapper mapper) : base(dbContextEvent, mapper)
    {
        _dbContext = dbContextEvent;
        _mapper = mapper;
    }

    public async Task<List<Event>> GetAllEventsByUserId(int userId)
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

    public async Task<Event?> GetEventById(int eventId)
    {
        EventDataModel? eventObj = await _dbContext.Events
                                                   .Include(eventObj => eventObj.EventCollaborators)
                                                     .ThenInclude(eventCollaborator => eventCollaborator.User)
                                                   .FirstOrDefaultAsync(eventObj => eventObj.Id == eventId);

        return _mapper.Map<Event>(eventObj);
    }

    public async Task<List<Event>> GetEventsWithinGivenDateByUserId(int userId, DateOnly startDate, DateOnly endDate)
    {
        List<EventDataModel> events = await _dbContext.Events
                                                      .Include(eventObj => eventObj.EventCollaborators)
                                                         .ThenInclude(eventCollaborator => eventCollaborator.User)
                                                      .Where(eventObj => eventObj
                                                                        .EventCollaborators
                                                                        .Where(eventCollaborator => eventCollaborator.EventDate >= startDate
                                                                               && eventCollaborator.EventDate <= endDate)
                                                      .Select(eventCollaborator => eventCollaborator.UserId)
                                                      .Contains(userId))
                                                      .ToListAsync();

        return _mapper.Map<List<Event>>(events);
    }
}
