using Core.Entities;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services;

public class EventCollaboratorService : IEventCollaboratorService
{
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;

    public EventCollaboratorService(IEventCollaboratorRepository eventCollaboratorRepository)
    {
        _eventCollaboratorRepository = eventCollaboratorRepository;
    }

    public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator)
    {
        return _eventCollaboratorRepository.Add(eventCollaborator);
    }

    public Task UpdateEventCollaborator(EventCollaborator eventCollaborator)
    {
        return _eventCollaboratorRepository.Update(eventCollaborator);
    }

    public Task DeleteEventCollaboratorsByEventId(int eventId)
    {
        return _eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(eventId);
    }
}
