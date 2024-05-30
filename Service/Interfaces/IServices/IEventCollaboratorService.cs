using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IEventCollaboratorService
{
    public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator);

    public Task UpdateEventCollaborator(EventCollaborator eventCollaborator);

    public Task DeleteEventCollaboratorsByEventId(int eventId);
}
