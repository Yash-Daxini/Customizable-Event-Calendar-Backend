using Core.Entities;
using Core.Entities.Enums;
using Core.Extensions;
using Core.Interfaces.IServices;

namespace Core.Services;

public class MultipleInviteesEventService : IMultipleInviteesEventService
{
    private readonly IEventService _eventService;
    private readonly IEventCollaboratorService _eventCollaboratorService;

    public MultipleInviteesEventService(IEventService eventService,
                                        IEventCollaboratorService eventCollaboratorService)
    {
        _eventService = eventService;
        _eventCollaboratorService = eventCollaboratorService;
    }

    public async Task StartSchedulingProcessOfProposedEvent(int userId)
    {
        List<Event> events = await _eventService.GetProposedEventsByUserId(userId);

        foreach (var eventObj in events)
        {
            int remainingDays = CalculateDayDifference(DateTime.Now,
                                                       eventObj.RecurrencePattern.StartDate
                                                       .ConvertToDateTime());

            if (remainingDays is <= 1)
            {
                int[] proposedHours = CalculateProposedHours(eventObj);
                Duration mutualTime = FindMaximumMutualTimeBlock(proposedHours, eventObj);
                UpdateEventDurationToMutualDuration(eventObj, mutualTime, userId);
                ScheduleProposedEvent(eventObj);
            }
            else if (!IsAnyInviteeWithPendingStatus(eventObj))
            {
                int[] proposedHours = CalculateProposedHours(eventObj);
                Duration mutualTime = FindMaximumMutualTimeBlock(proposedHours, eventObj);
                UpdateEventDurationToMutualDuration(eventObj, mutualTime, userId);
                UpdateEventCollaboratorsStatus(eventObj);
            }
        }
    }

    private static bool IsAnyInviteeWithPendingStatus(Event eventObj)
    {
        return eventObj.GetInviteesOfEvent()
                       .Exists(eventCollaborator => eventCollaborator.IsEventCollaboratorWithPendingStatus());
    }

    private static int CalculateDayDifference(DateTime firstDate, DateTime secondDate)
    {
        return Math.Abs((firstDate - secondDate).Days);
    }

    private int[] CalculateProposedHours(Event eventObj)
    {
        int[] proposedHours = new int[23];

        foreach (var eventCollaborator in GetInviteesWithProposedStatus(eventObj))
        {
            if (!eventCollaborator.IsNullProposedDuration())
                CountProposeHours(eventCollaborator.ProposedDuration.StartHour,
                                  eventCollaborator.ProposedDuration.EndHour,
                                  ref proposedHours);
        }

        return proposedHours;

    }

    private IEnumerable<EventCollaborator> GetInviteesWithProposedStatus(Event eventObj)
    {
        return eventObj.GetInviteesOfEvent()
                       .Where(eventCollaborator => eventCollaborator.IsEventCollaboratorWithProposedStatus());
    }

    private static void CountProposeHours(int startHour, int endHour, ref int[] proposedHours)
    {
        while (startHour < endHour)
        {
            proposedHours[startHour]++;
            startHour++;
        }
    }

    private Duration FindMaximumMutualTimeBlock(int[] proposedHours, Event eventObj)
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

        return new Duration()
        {
            StartHour = startHour,
            EndHour = endHour,
        };

    }

    private void ScheduleProposedEvent(Event eventObj)
    {
        DateOnly eventDate = eventObj.RecurrencePattern.StartDate;

        foreach (var eventCollaborator in eventObj.GetInviteesOfEvent().Where(IsInviteePresentInEvent))
        {
            if (eventCollaborator.IsEventCollaboratorWithProposedStatus())
                HandleInviteeThatProposedTime(eventObj, eventCollaborator);

            eventCollaborator.SetProposedDurationToNull();
            eventCollaborator.EventDate = eventDate;
            _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);
        }
    }

    private static void HandleInviteeThatProposedTime(Event eventObj, EventCollaborator eventCollaborator)
    {
        if (IsEventTimeWithInProposedTime(eventObj, eventCollaborator))
            eventCollaborator.AcceptConfirmationStatus();
        else
            eventCollaborator.RejectConfirmationStatus();
    }

    private static bool IsEventTimeWithInProposedTime(Event eventObj, EventCollaborator eventCollaborator)
    {
        return eventObj.Duration.StartHour >= eventCollaborator.ProposedDuration.StartHour
               && eventObj.Duration.EndHour <= eventCollaborator.ProposedDuration.EndHour;
    }

    private static bool IsInviteePresentInEvent(EventCollaborator eventCollaborator)
    {
        return eventCollaborator.IsEventCollaboratorWithAcceptStatus()
               || eventCollaborator.IsEventCollaboratorWithMaybeStatus()
               || eventCollaborator.IsEventCollaboratorWithProposedStatus();
    }

    private void UpdateEventCollaboratorsStatus(Event eventObj)
    {
        foreach (var eventCollaborator in eventObj.GetInviteesOfEvent())
        {
            if (eventCollaborator.IsOrganizerOfEvent())
            {
                eventCollaborator.SetProposedDurationToNull();
            }
            else
            {
                eventCollaborator.SetProposedDurationToNull();
                eventCollaborator.ConfirmationStatus = ConfirmationStatus.Pending;
            }
            _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);
        }
    }

    private void UpdateEventDurationToMutualDuration(Event eventObj, Duration mutualTime, int userId)
    {
        eventObj.Duration = mutualTime;

        _eventService.UpdateEvent(eventObj, userId);
    }

}
