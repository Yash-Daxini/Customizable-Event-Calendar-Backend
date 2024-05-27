using Core.Domain;
using Core.Interfaces.IServices;

namespace Core.Services;

public class OverlapEventService : IOverlappingEventService
{
    private readonly IRecurrenceService _recurrenceService;

    public OverlapEventService(IRecurrenceService recurrenceService)
    {
        _recurrenceService = recurrenceService;
    }

    public OverlapEventData? GetOverlappedEventInformation(Event eventForVerify, //TODO: Reduce parameters
                                                           List<Event> events,
                                                           List<DateOnly> occurrencesOfEventForVerify,
                                                           bool isInsert,
                                                           int userId)
    {

        foreach (var existingEvent in events)
        {
            if (!isInsert && existingEvent.Id == eventForVerify.Id) continue;

            List<DateOnly> occurrencesOfEventToCheckOverlap = _recurrenceService.GetOccurrencesOfEvent(existingEvent);

            DateOnly matchedDate = occurrencesOfEventToCheckOverlap.Intersect(occurrencesOfEventForVerify).FirstOrDefault();

            if (IsEventOverlapps(eventForVerify, existingEvent, matchedDate))
                return new OverlapEventData(eventForVerify, existingEvent, matchedDate);
        }

        return null;
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
