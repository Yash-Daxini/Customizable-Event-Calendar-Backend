using System.Text;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;

namespace Core.Services;

public class SharedEventCollaborationService : ISharedEventCollaborationService
{
    private readonly IEventCollaboratorService _eventCollaboratorService;
    private readonly IEventService _eventService;

    public SharedEventCollaborationService(IEventCollaboratorService eventCollaboratorService, IEventService eventService)
    {
        _eventCollaboratorService = eventCollaboratorService;
        _eventService = eventService;
    }

    public async Task AddCollaborator(EventCollaborator eventCollaborator)
    {
        bool isAlreadyCollaborated = await IsEventAlreadyCollaborated(eventCollaborator);

        if (isAlreadyCollaborated)
            throw new UserAlreadyCollaboratedException("Already collaborated in this event");

        List<Event> overlapEvents = await GetCollaborationOverlaps(eventCollaborator);

        if (overlapEvents.Count > 0)
            throw new CollaborationOverlapException(GetOverlapMessage(eventCollaborator.EventDate, overlapEvents));

        await _eventCollaboratorService.AddEventCollaborator(eventCollaborator);
    }

    private static string GetOverlapMessage(DateOnly date, List<Event> overlapEvents)
    {
        StringBuilder overlapMessage = new($"Collaboration overlaps with following events on {date} :- ");

        foreach (var eventObj in overlapEvents)
        {
            overlapMessage.AppendLine($"Event Name :- {eventObj.Title} Time :- {eventObj.Duration.GetDurationInFormat()}");
        }

        return overlapMessage.ToString();
    }

    private async Task<List<Event>> GetCollaborationOverlaps(EventCollaborator eventCollaborator)
    {
        Event eventToCollaborate = await _eventService.GetEventById(eventCollaborator.EventId, eventCollaborator.User.Id);

        DateOnly selectedEventDate = eventCollaborator.EventDate;

        List<Event> events = await _eventService
                                   .GetNonProposedEventsByUserId(eventCollaborator.User.Id);

        return [..events
               .Where(eventModel => eventModel.IsUserCollaboratedOnGivenDate(eventCollaborator.User.Id,selectedEventDate)
                                    && eventModel.Duration.IsOverlappingWith(eventToCollaborate.Duration))];
    }

    private async Task<bool> IsEventAlreadyCollaborated(EventCollaborator eventCollaborator)
    {
        Event eventModelToCheckOverlap = await _eventService.GetEventById(eventCollaborator.EventId, eventCollaborator.User.Id);

        DateOnly selectedEventDate = eventCollaborator.EventDate;

        return eventModelToCheckOverlap.IsUserCollaboratedOnGivenDate(eventCollaborator.User.Id, selectedEventDate);
    }
}
