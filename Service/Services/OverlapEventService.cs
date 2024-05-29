using Core.Domain.Models;
using Core.Interfaces.IServices;

namespace Core.Services;

public class OverlapEventService : IOverlappingEventService
{
    public OverlapEventData? GetOverlappedEventInformation(Event eventForVerify, List<Event> events)
    {
        Dictionary<Event, DateOnly> overlapEventByDate = [];

        foreach (var existingEvent in events)
        {
            if (existingEvent.Id == eventForVerify.Id) continue;

            List<DateOnly> occurrencesOfExistingEvent = [..existingEvent.DateWiseParticipants
                                                          .Select(participantByDate => participantByDate.EventDate)];

            List<DateOnly> occurrencesOfEventForVerify = [..eventForVerify.DateWiseParticipants
                                                            .Select(participantByDate => participantByDate.EventDate)];

            DateOnly matchedDate = occurrencesOfExistingEvent.Intersect(occurrencesOfEventForVerify).FirstOrDefault();

            if (IsEventOverlapps(eventForVerify, existingEvent, matchedDate))
                overlapEventByDate.Add(existingEvent, matchedDate);
        }


        return overlapEventByDate.Count == 0
               ? null
               : new OverlapEventData(eventForVerify, overlapEventByDate);
    }

    private static bool IsEventOverlapps(Event eventForVerify, Event existingEvent, DateOnly matchedDate)
    {
        if (matchedDate == default) return false;

        if (IsHourOverlaps(eventForVerify.Duration.StartHour,
                           eventForVerify.Duration.EndHour,
                           existingEvent.Duration.StartHour,
                           existingEvent.Duration.EndHour))
            return true;

        return false;
    }

    private static bool IsHourOverlaps(int startHourOfFirstEvent, int endHourOfFirstEvent, int startHourOfSecondEvent, int endHourOfSecondEvent)
    {
        return (startHourOfFirstEvent >= startHourOfSecondEvent && startHourOfFirstEvent < endHourOfSecondEvent)
            || (endHourOfFirstEvent > startHourOfSecondEvent && endHourOfFirstEvent <= endHourOfSecondEvent)
            || (startHourOfSecondEvent >= startHourOfFirstEvent && startHourOfSecondEvent < endHourOfFirstEvent)
            || (endHourOfSecondEvent > startHourOfFirstEvent && endHourOfSecondEvent <= endHourOfFirstEvent);
    }
}
