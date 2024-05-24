using Core.Domain;

namespace Core.Interfaces;

public interface IRecurrenceService
{
    public Task ScheduleEvents(Event eventModel, List<Participant> participants);

    public List<DateOnly> GetOccurrencesOfEvent(Event eventModel);
}
