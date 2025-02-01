using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using Core.Models;

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

        OverlapResponseModel overlapResponseModel = GetOverlapMessage(eventForVerify, overlapEventByDate);

        if (overlapEventByDate.Count is not 0)
            throw new EventOverlapException(overlapResponseModel);
    }

    private OverlapResponseModel GetOverlapMessage(Event CheckingEvent, Dictionary<Event, DateOnly> OverlappingEventsByDate)
    {
        StringBuilder overlapMessage = new($"{CheckingEvent.Title} overlaps with following events at " +
                                           $"{CheckingEvent.Duration.GetDurationInFormat()} :-  ");

        OverlapResponseModel overlapResponseModel = new OverlapResponseModel()
        {
            Title = CheckingEvent.Title,
            OverlapEvents = []
        };

        foreach (var (overlapEvent, matchedDate) in OverlappingEventsByDate.Select(e => (e.Key, e.Value)))
        {
            overlapResponseModel.OverlapEvents.Add(new OverlapEventModel()
            {
                Title = overlapEvent.Title,
                Date = matchedDate,
                Duration = overlapEvent.Duration,
            });
        }

        return overlapResponseModel;
    }
}
