using Core.Entities.Enums;

namespace Core.Entities;

abstract public class RecurrencePattern
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Frequency Frequency { get; set; }

    public int Interval { get; set; }

    public List<int>? ByWeekDay { get; set; }

    abstract public int GetOccurrencesCount();

    abstract public List<DateOnly> GetOccurrences();
}
