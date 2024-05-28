using Core.Domain;

namespace Core.Interfaces.IRepositories;

public interface IEventCollaboratorRepository
{
    public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator);

    public Task<int> UpdateEventCollaborator(EventCollaborator eventCollaborator);

    public Task DeleteEventCollaboratorsByEventId(int eventId);
}
