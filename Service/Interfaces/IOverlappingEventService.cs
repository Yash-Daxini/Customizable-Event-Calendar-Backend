using Core.Domain;

namespace Core.Interfaces;

public interface IOverlappingEventService
{
    public OverlapEventData? GetOverlappedEventInformation(Event eventForVerify,
                                                           List<Event> events,
                                                           List<DateOnly> occurrencesOfEventForVerify,
                                                           bool isInsert,
                                                           int userId);
}
