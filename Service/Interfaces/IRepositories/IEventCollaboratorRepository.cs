using Core.Entities;

namespace Core.Interfaces.IRepositories;

public interface IEventCollaboratorRepository : IRepository<EventCollaborator>
{
    public Task DeleteEventCollaboratorsByEventId(int eventId);

    public Task<EventCollaborator?> GetEventCollaboratorById(int eventCollaboratorId);
}
