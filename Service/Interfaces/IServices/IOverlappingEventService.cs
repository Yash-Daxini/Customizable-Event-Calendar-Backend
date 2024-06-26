using Core.Entities;

namespace Core.Interfaces.IServices;

public interface IOverlappingEventService
{
    public void CheckOverlap(Event eventForVerify, List<Event> events);
}
