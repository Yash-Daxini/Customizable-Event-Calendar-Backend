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
        return eventObj.GetEventInvitees()
                       .Exists(eventCollaborator => eventCollaborator.IsStatusPending());
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
            if (!eventCollaborator.IsProposedDurationNull())
                CountProposedHours(eventCollaborator.ProposedDuration.StartHour,
                                  eventCollaborator.ProposedDuration.EndHour,
                                  ref proposedHours);
        }

        return proposedHours;

    }

    private IEnumerable<EventCollaborator> GetInviteesWithProposedStatus(Event eventObj)
    {
        return eventObj.GetEventInvitees()
                       .Where(eventCollaborator => eventCollaborator.IsStatusProposed());
    }

    private static void CountProposedHours(int startHour, int endHour, ref int[] proposedHours)
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

        return new Duration(startHour, endHour);

    }

    private void ScheduleProposedEvent(Event eventObj)
    {
        DateOnly eventDate = eventObj.RecurrencePattern.StartDate;

        List<EventCollaborator> eventCollaborators = [.. eventObj
                                                     .GetEventInvitees()
                                                     .Where(IsInviteePresentInEvent)];

        foreach (var eventCollaborator in eventCollaborators)
        {
            if (eventCollaborator.IsStatusProposed())
                HandleInviteeThatProposedTime(eventObj, eventCollaborator);

            eventCollaborator.ResetProposedDuration();
            eventCollaborator.EventDate = eventDate;
            _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);
        }
    }

    private static void HandleInviteeThatProposedTime(Event eventObj, EventCollaborator eventCollaborator)
    {
        if (IsEventTimeWithInProposedTime(eventObj, eventCollaborator))
            eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Accept);
        else
            eventCollaborator.SetConfirmationStatus(ConfirmationStatus.Reject);
    }

    private static bool IsEventTimeWithInProposedTime(Event eventObj, EventCollaborator eventCollaborator)
    {
        return eventObj.Duration.StartHour >= eventCollaborator.ProposedDuration.StartHour
               && eventObj.Duration.EndHour <= eventCollaborator.ProposedDuration.EndHour;
    }

    private static bool IsInviteePresentInEvent(EventCollaborator eventCollaborator)
    {
        return eventCollaborator.IsStatusAccept()
               || eventCollaborator.IsStatusMaybe()
               || eventCollaborator.IsStatusProposed();
    }

    private void UpdateEventCollaboratorsStatus(Event eventObj)
    {
        foreach (var eventCollaborator in eventObj.GetEventInvitees())
        {
            if (!eventCollaborator.IsOrganizer())
            {
                eventCollaborator.ConfirmationStatus = ConfirmationStatus.Pending;
            }
            eventCollaborator.ResetProposedDuration();
            _eventCollaboratorService.UpdateEventCollaborator(eventCollaborator);
        }
    }

    private void UpdateEventDurationToMutualDuration(Event eventObj, Duration mutualTime, int userId)
    {
        eventObj.Duration = mutualTime;

        _eventService.UpdateEvent(eventObj, userId);
    }

}
