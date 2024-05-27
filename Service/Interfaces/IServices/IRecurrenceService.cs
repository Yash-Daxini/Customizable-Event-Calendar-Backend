using Core.Domain;

namespace Core.Interfaces.IServices;

public interface IRecurrenceService
{
    public List<DateOnly> GetOccurrencesOfEvent(Event eventModel);
}
