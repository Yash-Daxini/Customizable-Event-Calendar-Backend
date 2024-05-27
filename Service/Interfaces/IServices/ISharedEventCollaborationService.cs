using Core.Domain;

namespace Core.Interfaces.IServices;

public interface ISharedEventCollaborationService
{
    public Task AddCollaborator(Participant participant);
}
