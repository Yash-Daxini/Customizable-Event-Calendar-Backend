using Core.Domain.Models;

namespace Core.Interfaces.IServices;

public interface IOverlappingEventService
{
    public OverlapEventData? GetOverlappedEventInformation(Event eventForVerify, List<Event> events);
}
