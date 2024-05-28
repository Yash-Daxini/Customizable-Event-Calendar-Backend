using Core.Domain;
using Core.Domain.Enums;
using Core.Interfaces.IServices;

namespace Core.Services;

public class MultipleInviteesEventService : IMultipleInviteesEventService
{
    private readonly IEventService _eventService;
    private readonly IEventCollaboratorService _participantService;

    public MultipleInviteesEventService(IEventService eventService,
                                        IEventCollaboratorService participantService)
    {
        _eventService = eventService;
        _participantService = participantService;
    }

    public async Task StartSchedulingProcessOfProposedEvent(int userId)
    {
        List<Event> events = await _eventService.GetProposedEventsByUserId(userId);

        foreach (var eventObj in events)
        {
            int remainingDays = CalculateDayDifference(DateTime.Now, DateTime.Parse(eventObj.RecurrencePattern.StartDate.ToString()));

            if (remainingDays is <= 1)
            {
                int[] proposedHours = CalculateProposedHours(eventObj);
                FindMaximumMutualTimeBlock(proposedHours, eventObj);
                ScheduleProposedEvent(eventObj);
            }
            else if (!IsAnyInviteeWithPendingStatus(eventObj))
            {
                int[] proposedHours = CalculateProposedHours(eventObj);
                FindMaximumMutualTimeBlock(proposedHours, eventObj);
                UpdateParticipantsStatus(eventObj);
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

    private int[] CalculateProposedHours(Event eventObj)
    {
        int[] proposedHours = new int[23];

        foreach (var participant in GetInviteesWithProposedStatus(eventObj))
        {
            if (!participant.IsNullProposedDuration())
                CountProposeHours(participant.ProposedDuration.StartHour, participant.ProposedDuration.EndHour, ref proposedHours);
        }

        return proposedHours;

    }

    private IEnumerable<EventCollaborator> GetInviteesWithProposedStatus(Event eventObj)
    {
        return eventObj.GetInviteesOfEvent().Where(participant => participant.IsEventCollaboratorWithProposedStatus());
    }

    private static void CountProposeHours(int startHour, int endHour, ref int[] proposedHours)
    {
        while (startHour < endHour)
        {
            proposedHours[startHour]++;
            startHour++;
        }
    }

    private void FindMaximumMutualTimeBlock(int[] proposedHours, Event eventObj)
    {
        int max = proposedHours.Max();
        max = max > 1 ? max : -1;
        int startHour = -1;
        int endHour = -1;
        int timeBlock = eventObj.Duration.EndHour - eventObj.Duration.StartHour;

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
            startHour = eventObj.Duration.StartHour;
            endHour = eventObj.Duration.EndHour;
        }

        UpdateEventDurationToMutualDuration(eventObj, startHour, endHour);

    }

    private void ScheduleProposedEvent(Event eventObj)
    {
        DateOnly eventDate = eventObj.RecurrencePattern.StartDate;

        foreach (var participant in eventObj.GetInviteesOfEvent().Where(IsInviteePresentInEvent))
        {
            if (participant.IsEventCollaboratorWithProposedStatus())
                HandleInviteeThatProposedTime(eventObj, participant);

            participant.ProposedDuration = null;
            participant.EventDate = eventDate;
            _participantService.UpdateEventCollaborator(participant);
        }
    }

    private static void HandleInviteeThatProposedTime(Event eventObj, EventCollaborator participant)
    {
        if (IsEventTimeWithInProposedTime(eventObj, participant))
            participant.ConfirmationStatus = ConfirmationStatus.Accept;
        else
            participant.ConfirmationStatus = ConfirmationStatus.Reject;
    }

    private static bool IsEventTimeWithInProposedTime(Event eventObj, EventCollaborator participant)
    {
        return eventObj.Duration.StartHour >= participant.ProposedDuration.StartHour
               && eventObj.Duration.EndHour <= participant.ProposedDuration.EndHour;
    }

    private static bool IsInviteePresentInEvent(EventCollaborator participant)
    {
        return participant.IsEventCollaboratorWithAcceptStatus()
               || participant.IsEventCollaboratorWithMaybeStatus()
               || participant.IsEventCollaboratorWithProposedStatus();
    }

    private void UpdateParticipantsStatus(Event eventObj)
    {
        foreach (var eventCollaborator in eventObj.GetInviteesOfEvent())
        {
            if (eventCollaborator.IsOrganizerOfEvent())
            {
                eventCollaborator.ProposedDuration = null;
            }
            else
            {
                eventCollaborator.ProposedDuration = null;
                eventCollaborator.ConfirmationStatus = ConfirmationStatus.Pending;
            }
            _participantService.UpdateEventCollaborator(eventCollaborator);
        }
    }

    private void UpdateEventDurationToMutualDuration(Event eventObj, int newStartHour, int newEndHour)
    {
        eventObj.Duration.StartHour = newStartHour;
        eventObj.Duration.EndHour = newEndHour;

        _eventService.UpdateEvent(eventObj);
    }

}
