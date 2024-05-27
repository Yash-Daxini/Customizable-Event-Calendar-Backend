﻿using Core.Domain;
using Core.Interfaces.IServices;

namespace Core.Services;

public class SharedEventCollaborationService : ISharedEventCollaborationService
{
    private readonly IParticipantService _participantService;
    private readonly IEventService _eventService;

    public SharedEventCollaborationService(IParticipantService participantService, IEventService eventService)
    {
        _participantService = participantService;
        _eventService = eventService;
    }

    public async Task AddCollaborator(Participant participant)
    {
        bool isAlreadyCollaborated = await IsEventAlreadyCollaborated(participant);

        if (isAlreadyCollaborated)
            throw new Exception("Already collaborated in this event");

        Event? overlapEvent = await GetCollaborationOverlap(participant);

        if (overlapEvent is not null)
            throw new Exception($"Overlaps with {overlapEvent.Title} at " +
                                $"{participant.EventDate} from " +
                                $"{overlapEvent.Duration.GetDurationInFormat()}");

        await _participantService.AddParticipant(participant);
    }

    private async Task<Event?> GetCollaborationOverlap(Participant participant) //TODO : What if multiple events overlap
    {

        Event? eventToCollaborate = await _eventService.GetEventById(participant.EventId);

        DateOnly selectedEventDate = participant.EventDate;

        List<Event> events = await _eventService
                                   .GetNonProposedEventsByUserId(participant.User.Id);

        return events
               .Find(eventModel => eventModel
                                   .DateWiseParticipants
                                   .Exists(participantByDate => participantByDate.EventDate == selectedEventDate
                                           && participantByDate.Participants
                                                               .Exists(participant => participant.User.Id == participant.User.Id))
                                    && IsHourOvelapps(eventModel.Duration.StartHour,
                                                     eventModel.Duration.EndHour,
                                                     eventToCollaborate.Duration.StartHour,
                                                     eventToCollaborate.Duration.EndHour));
    }

    private async Task<bool> IsEventAlreadyCollaborated(Participant participant)
    {

        Event? eventModelToCheckOverlap = await _eventService.GetEventById(participant.EventId);

        if (eventModelToCheckOverlap is null) return false;

        DateOnly selectedEventDate = participant.EventDate;

        ParticipantsByDate? participantByDate = eventModelToCheckOverlap
                                                .DateWiseParticipants
                                                .Find(participantByDate => participantByDate.EventDate == selectedEventDate);

        if (participantByDate is null) return false;

        return participantByDate.Participants.Exists(participantOfEvent => participantOfEvent.User.Id == participant.User.Id);
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
