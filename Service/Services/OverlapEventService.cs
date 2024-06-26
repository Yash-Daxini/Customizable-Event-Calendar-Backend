using System.Text;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;

namespace Core.Services;

public class OverlapEventService : IOverlappingEventService
{
    public void CheckOverlap(Event eventForVerify, List<Event> events)
    {
        Dictionary<Event, DateOnly> overlapEventByDate = [];

        if (eventForVerify is null || events.Count is 0)
            return;

        foreach (var existingEvent in events)
        {
            if (existingEvent.Id == eventForVerify.Id) continue;

            DateOnly? matchedDate = eventForVerify.GetOverlapDate(existingEvent);

            if (eventForVerify.IsEventOverlappingWith(existingEvent, matchedDate))
                overlapEventByDate.Add(existingEvent, (DateOnly)matchedDate);
        }

        string overlapInformation = GetOverlapMessage(eventForVerify, overlapEventByDate);

        if (overlapEventByDate.Count is not 0)
            throw new EventOverlapException($" {overlapInformation}");
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
