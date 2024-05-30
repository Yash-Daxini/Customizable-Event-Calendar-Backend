using Core.Entities;

namespace Core.Interfaces.IServices;

public interface ISharedEventCollaborationService
{
    public Task AddCollaborator(EventCollaborator eventCollaborator);
}
