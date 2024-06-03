using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IRepositories;
using Core.Interfaces.IServices;

namespace Core.Services;

public class EventCollaboratorService : IEventCollaboratorService
{
    private readonly IEventCollaboratorRepository _eventCollaboratorRepository;
    private readonly IEventService _eventService;

    public EventCollaboratorService(IEventCollaboratorRepository eventCollaboratorRepository, IEventService eventService)
    {
        _eventCollaboratorRepository = eventCollaboratorRepository;
        _eventService = eventService;
    }

    public Task<int> AddEventCollaborator(EventCollaborator eventCollaborator)
    {
        return _eventCollaboratorRepository.Add(eventCollaborator);
    }

    public async Task UpdateEventCollaborator(EventCollaborator eventCollaborator)
    {
        EventCollaborator? eventCollaboratorById = await _eventCollaboratorRepository.GetEventCollaboratorById(eventCollaborator.Id);

        if (eventCollaboratorById is null)
            throw new NotFoundException($"Event collaborator with Id ${eventCollaborator.Id} not present!");

        eventCollaborator.SetEventCollaboratorRoleAsParticipant();
        await _eventCollaboratorRepository.Update(eventCollaborator);
    }

    public async Task DeleteEventCollaboratorsByEventId(int eventId, int userId)
    {
        Event? eventObj = await _eventService.GetEventById(eventId, userId);

        if (eventObj is null)
            throw new NotFoundException($"Event with Id ${eventId} not present!");

        await _eventCollaboratorRepository.DeleteEventCollaboratorsByEventId(eventId);
    }
}
