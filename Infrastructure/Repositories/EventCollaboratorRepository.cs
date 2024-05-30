using Infrastructure.DataModels;
using AutoMapper;
using Core.Interfaces.IRepositories;
using Core.Entities;

namespace Infrastructure.Repositories;

public class EventCollaboratorRepository : BaseRepository<EventCollaborator, EventCollaboratorDataModel>, IEventCollaboratorRepository
{
    private readonly DbContextEventCalendar _dbContext;

    public EventCollaboratorRepository(DbContextEventCalendar dbContextEvent, IMapper mapper) : base(dbContextEvent, mapper)
    {
        _dbContext = dbContextEvent;
    }

    public async Task DeleteEventCollaboratorsByEventId(int eventId)
    {
        List<EventCollaboratorDataModel> eventCollaboratorsToDelete = [.._dbContext
                                                                      .EventCollaborators
                                                                      .Where(eventcollaborator => eventcollaborator.EventId == eventId)];

        _dbContext.RemoveRange(eventCollaboratorsToDelete);

        await _dbContext.SaveChangesAsync();
    }
}
