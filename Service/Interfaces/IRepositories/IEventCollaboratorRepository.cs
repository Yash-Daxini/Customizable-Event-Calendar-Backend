using Core.Domain.Models;

namespace Core.Interfaces.IRepositories;

public interface IEventCollaboratorRepository : IRepository<EventCollaborator>
{
    public Task DeleteEventCollaboratorsByEventId(int eventId);
}
