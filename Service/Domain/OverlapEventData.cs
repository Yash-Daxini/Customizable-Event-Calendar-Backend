namespace Core.Domain;

public class OverlapEventData(Event eventToVerifyOverlap, Event overlappedEvent, DateOnly matchedDate)
{
    public Event CheckingEvent { get; set; } = eventToVerifyOverlap;

    public Event OverlappingEvent { get; set; } = overlappedEvent;

    public DateOnly OverlappedDate { get; set; } = matchedDate;

    public string GetOverlapMessage()
    {
        return $"\"{CheckingEvent.Title}\" overlaps with \"{OverlappingEvent.Title}\" at {OverlappedDate} on following duration\n" +
               $"1. {CheckingEvent.Duration.GetDurationInFormat()} \n" +
               $"overlaps with " +
               $"\n2. {OverlappingEvent.Duration.GetDurationInFormat()} \n";
    }
}
