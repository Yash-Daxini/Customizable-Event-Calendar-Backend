using System.Text;
using Core.Entities;
using Core.Interfaces.IServices;

namespace Core.Services;

public class OverlapEventService : IOverlappingEventService
{
    public string? GetOverlappedEventInformation(Event eventForVerify, List<Event> events)
    {
        Dictionary<Event, DateOnly> overlapEventByDate = [];

        if(eventForVerify is null || events.Count is 0)
            return null;

        foreach (var existingEvent in events)
        {
            if (existingEvent.Id == eventForVerify.Id) continue;

            DateOnly? matchedDate = eventForVerify.GetOverlapDate(existingEvent);

            if (eventForVerify.IsEventOverlappingWith(existingEvent, matchedDate))
                overlapEventByDate.Add(existingEvent, (DateOnly)matchedDate);
        }

        return overlapEventByDate.Count == 0
               ? null
               : GetOverlapMessage(eventForVerify, overlapEventByDate);
    }

    private string GetOverlapMessage(Event CheckingEvent, Dictionary<Event, DateOnly> OverlappingEventsByDate)
    {
        StringBuilder overlapMessage = new($"{CheckingEvent.Title} overlaps with following events at " +
                                           $"{CheckingEvent.Duration.GetDurationInFormat()} :-  ");

        foreach (var (overlapEvent, matchedDate) in OverlappingEventsByDate.Select(e => (e.Key, e.Value)))
        {
            overlapMessage.AppendLine($"Event Name : {overlapEvent.Title} , " +
                                      $"Date : {matchedDate} , " +
                                      $"Duration : {overlapEvent.Duration.GetDurationInFormat()}");
        }

        return overlapMessage.ToString();
    }
}
