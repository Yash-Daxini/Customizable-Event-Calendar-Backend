using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IOverlappingEventService
{
    public string? GetOverlappedEventInformation(Event eventForVerify, List<Event> events);
}
