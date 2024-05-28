using System.Text;

namespace Core.Domain;

public class OverlapEventData(Event eventToVerifyOverlap, Dictionary<Event,DateOnly> overlappedEventsByDate)
{
    public Event CheckingEvent { get; set; } = eventToVerifyOverlap;

    public Dictionary<Event,DateOnly> OverlappingEventsByDate { get; set; } = overlappedEventsByDate;

    public string GetOverlapMessage()
    {
        StringBuilder overlapMessage = new ($"\"{CheckingEvent.Title}\" overlaps with following events at " +
                                                $"{CheckingEvent.Duration.GetDurationInFormat()} :-");

        foreach (var (overlapEvent,matchedDate) in OverlappingEventsByDate.Select(e => (e.Key,e.Value)))
        {
            overlapMessage.AppendLine($"Event Name : {overlapEvent.Title} Date : {matchedDate} " +
                                      $"Duration : {overlapEvent.Duration.GetDurationInFormat()}");
        }

        return overlapMessage.ToString();
    }
}
