using Core.Domain;

namespace Core.Interfaces.IServices;

public interface IEventCollaboratorService
{
    public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator);

    public Task<int> UpdateEventCollaborator(EventCollaborator eventCollaborator);

    public Task DeleteEventCollaboratorsByEventId(int eventId);
}
