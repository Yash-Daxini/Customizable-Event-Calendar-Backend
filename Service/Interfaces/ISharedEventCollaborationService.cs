using Core.Domain;

namespace Core.Interfaces;

public interface ISharedEventCollaborationService
{
    public Task AddCollaborator(Participant participant, int eventId);

    public Task<bool> IsEventAlreadyCollaborated(Participant participant, int eventId);

    public Task<Event?> GetCollaborationOverlap(Participant participant, int eventId);
}
