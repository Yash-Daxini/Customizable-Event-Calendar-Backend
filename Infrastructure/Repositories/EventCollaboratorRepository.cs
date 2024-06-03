using Infrastructure.DataModels;
using AutoMapper;
using Core.Interfaces.IRepositories;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class EventCollaboratorRepository : BaseRepository<EventCollaborator, EventCollaboratorDataModel>, IEventCollaboratorRepository
{
    private readonly DbContextEventCalendar _dbContext;
    private readonly IMapper _mapper;

    public EventCollaboratorRepository(DbContextEventCalendar dbContextEvent, IMapper mapper) : base(dbContextEvent, mapper)
    {
        _dbContext = dbContextEvent;
        _mapper = mapper;
    }

    public async Task DeleteEventCollaboratorsByEventId(int eventId)
    {
        List<EventCollaboratorDataModel> eventCollaboratorsToDelete = [.._dbContext
                                                                      .EventCollaborators
                                                                      .Where(eventcollaborator => eventcollaborator.EventId == eventId)];

        _dbContext.RemoveRange(eventCollaboratorsToDelete);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<EventCollaborator?> GetEventCollaboratorById(int eventCollaboratorId)
    {
        EventCollaboratorDataModel? eventCollaborator = await _dbContext.EventCollaborators
                                                             .Include(e => e.User)
                                                             .FirstOrDefaultAsync(eventCollaborator => eventCollaborator.Id == eventCollaboratorId);

        return _mapper.Map<EventCollaborator>(eventCollaborator);

    }
}
