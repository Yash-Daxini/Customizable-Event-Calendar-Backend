using System.Text;
using Core.Domain.Models;
using Core.Exceptions;
using Core.Interfaces.IServices;

namespace Core.Services;

public class SharedEventCollaborationService : ISharedEventCollaborationService
{
    private readonly IEventCollaboratorService _participantService;
    private readonly IEventService _eventService;

    public SharedEventCollaborationService(IEventCollaboratorService participantService, IEventService eventService)
    {
        _participantService = participantService;
        _eventService = eventService;
    }

    public async Task AddCollaborator(EventCollaborator participant)
    {
        bool isAlreadyCollaborated = await IsEventAlreadyCollaborated(participant);

        if (isAlreadyCollaborated)
            throw new UserAlreadyCollaboratedException("Already collaborated in this event");

        List<Event> overlapEvents = await GetCollaborationOverlaps(participant);

        if (overlapEvents.Count > 0)
            throw new CollaborationOverlapException(GetOverlapMessage(participant.EventDate, overlapEvents));

        await _participantService.AddEventCollaborator(participant);
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

    private async Task<List<Event>> GetCollaborationOverlaps(EventCollaborator participant)
    {

        Event? eventToCollaborate = await _eventService.GetEventById(participant.EventId);

        DateOnly selectedEventDate = participant.EventDate;

        List<Event> events = await _eventService
                                   .GetNonProposedEventsByUserId(participant.User.Id);

        return [..events
               .Where(eventModel => eventModel
                                   .DateWiseParticipants
                                   .Exists(participantByDate => participantByDate.EventDate == selectedEventDate
                                           && participantByDate.EventCollaborators
                                                               .Exists(participant => participant.User.Id == participant.User.Id))
                                    && IsHourOvelapps(eventModel.Duration.StartHour,
                                                     eventModel.Duration.EndHour,
                                                     eventToCollaborate.Duration.StartHour,
                                                     eventToCollaborate.Duration.EndHour))];
    }

    private async Task<bool> IsEventAlreadyCollaborated(EventCollaborator participant)
    {

        Event? eventModelToCheckOverlap = await _eventService.GetEventById(participant.EventId);

        if (eventModelToCheckOverlap is null) return false;

        DateOnly selectedEventDate = participant.EventDate;

        EventCollaboratorsByDate? participantByDate = eventModelToCheckOverlap
                                                .DateWiseParticipants
                                                .Find(participantByDate => participantByDate.EventDate == selectedEventDate);

        if (participantByDate is null) return false;

        return participantByDate.EventCollaborators.Exists(participantOfEvent => participantOfEvent.User.Id == participant.User.Id);
    }

    private static bool IsHourOvelapps(int startHourOfFirstEvent, int endHourOfFirstEvent, int startHourOfSecondEvent, int endHourOfSecondEvent)
    {
        return (startHourOfFirstEvent >= startHourOfSecondEvent
                && startHourOfFirstEvent < endHourOfSecondEvent)
            || (endHourOfFirstEvent > startHourOfSecondEvent
                && endHourOfFirstEvent <= endHourOfSecondEvent)
            || (startHourOfSecondEvent >= startHourOfFirstEvent
                && startHourOfSecondEvent < endHourOfFirstEvent)
            || (endHourOfSecondEvent > startHourOfFirstEvent
                && endHourOfSecondEvent <= endHourOfFirstEvent);
    }
}
