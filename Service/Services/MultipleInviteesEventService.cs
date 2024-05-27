using Core.Domain;
using Core.Domain.Enums;
using Core.Interfaces.IServices;

namespace Core.Services;

public class MultipleInviteesEventService : IMultipleInviteesEventService
{
    private readonly IEventService _eventService;
    private readonly IEventCollaboratorService _participantService;
    private readonly IRecurrenceService _recurrenceService;

    public MultipleInviteesEventService(IEventService eventService,
                                        IEventCollaboratorService participantService,
                                        IRecurrenceService recurrenceService)
    {
        _eventService = eventService;
        _participantService = participantService;
        _recurrenceService = recurrenceService;
    }

    public async Task StartSchedulingProcessOfProposedEvent()
    {
        List<Event> events = await _eventService.GetProposedEvents();

        foreach (var Event in events)
        {
            int remainingDays = CalculateDayDifference(DateTime.Now, DateTime.Parse(Event.RecurrencePattern.StartDate.ToString()));

            if (remainingDays is <= 1)
            {
                CalculateMutualTime(Event);
                ScheduleProposedEvent(Event);
            }
            else if (!IsAnyInviteeWithPendingStatus(Event))
            {
                CalculateMutualTime(Event);
                UpdateParticipantsStatus(Event);
            }
        }
    }

    private static bool IsAnyInviteeWithPendingStatus(Event eventObj)
    {
        return eventObj.GetInviteesOfEvent().Exists(participant => participant.IsEventCollaboratorWithPendingStatus());
    }

    private static int CalculateDayDifference(DateTime firstDate, DateTime secondDate)
    {
        return Math.Abs((firstDate - secondDate).Days);
    }

    private void CalculateMutualTime(Event Event)
    {
        int[] proposedHours = new int[23];

        foreach (var participant in GetInviteesWithProposedStatus(Event))
        {
            if (!participant.IsNullProposedDuration())
                CountProposeHours(participant.ProposedDuration.StartHour, participant.ProposedDuration.EndHour, ref proposedHours);
        }

        FindMaximumMutualTimeBlock(proposedHours, Event);

    }

    private IEnumerable<EventCollaborator> GetInviteesWithProposedStatus(Event Event)
    {
        return Event.GetInviteesOfEvent().Where(participant => participant.IsEventCollaboratorWithProposedStatus());
    }

    private static void CountProposeHours(int startHour, int endHour, ref int[] proposedHours)
    {
        while (startHour < endHour)
        {
            proposedHours[startHour]++;
            startHour++;
        }
    }

    private void FindMaximumMutualTimeBlock(int[] proposedHours, Event Event)
    {
        int max = proposedHours.Max();
        max = max > 1 ? max : -1;
        int startHour = -1;
        int endHour = -1;
        int timeBlock = Event.Duration.EndHour - Event.Duration.StartHour;

        for (int i = 0; i < proposedHours.Length; i++)
        {
            if (proposedHours[i] == max && startHour == -1)
            {
                startHour = i;
                endHour = i + 1;
                int j = endHour;

                while (j < proposedHours.Length && (endHour - startHour) < timeBlock)
                {
                    if (proposedHours[j] == max) endHour++;
                    else break;
                    j++;
                }
            }
        }

        if (startHour == -1)
        {
            startHour = Event.Duration.StartHour;
            endHour = Event.Duration.EndHour;
        }

        UpdateEventDurationToMutualDuration(Event, startHour, endHour);

    }

    private void ScheduleProposedEvent(Event Event)
    {
        DateOnly eventDate = Event.RecurrencePattern.StartDate;

        foreach (var participant in Event.GetInviteesOfEvent().Where(IsInviteePresentInEvent))
        {
            if (participant.IsEventCollaboratorWithProposedStatus())
                HandleInviteeThatProposedTime(Event, participant);

            participant.ProposedDuration = null;
            participant.EventDate = eventDate;
            _participantService.UpdateEventCollaborator(participant);
        }
    }

    private static void HandleInviteeThatProposedTime(Event Event, EventCollaborator participant)
    {
        if (IsEventTimeWithInProposedTime(Event, participant))
            participant.ConfirmationStatus = ConfirmationStatus.Accept;
        else
            participant.ConfirmationStatus = ConfirmationStatus.Reject;
    }

    private static bool IsEventTimeWithInProposedTime(Event Event, EventCollaborator participant)
    {
        return Event.Duration.StartHour >= participant.ProposedDuration.StartHour
               && Event.Duration.EndHour <= participant.ProposedDuration.EndHour;
    }

    private static bool IsInviteePresentInEvent(EventCollaborator participant)
    {
        return participant.IsEventCollaboratorWithAcceptStatus()
               || participant.IsEventCollaboratorWithMaybeStatus()
               || participant.IsEventCollaboratorWithProposedStatus();
    }

    private void UpdateParticipantsStatus(Event Event)
    {
        foreach (var participant in Event.GetInviteesOfEvent())
        {
            if (participant.IsOrganizerOfEvent())
            {
                participant.ProposedDuration = null;
            }
            else
            {
                participant.ProposedDuration = null;
                participant.ConfirmationStatus = ConfirmationStatus.Pending;
            }
            _participantService.UpdateEventCollaborator(participant);
        }
    }

    private void UpdateEventDurationToMutualDuration(Event eventObj, int newStartHour, int newEndHour)
    {
        eventObj.Duration.StartHour = newStartHour;
        eventObj.Duration.EndHour = newEndHour;

        _eventService.UpdateEvent(eventObj);
    }

}
