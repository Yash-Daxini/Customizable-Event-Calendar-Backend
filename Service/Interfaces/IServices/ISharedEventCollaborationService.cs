using Core.Domain.Models;

namespace Core.Interfaces.IServices;

public interface ISharedEventCollaborationService
{
    public Task AddCollaborator(EventCollaborator participant);
}
