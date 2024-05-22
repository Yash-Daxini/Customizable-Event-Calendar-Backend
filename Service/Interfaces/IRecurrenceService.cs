using Core.Domain;

namespace Core.Interfaces;

public interface IRecurrenceService
{
    public void ScheduleEvents(Event eventModel, List<Participant> participants);
}
