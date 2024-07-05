using Core.Entities.Enums;

namespace Core.Entities.RecurrecePattern;

public abstract class RecurrencePattern
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Frequency Frequency { get; set; }

    public int Interval { get; set; }

    public List<int>? ByWeekDay { get; set; }

    public abstract int GetOccurrencesCount();

    public abstract List<DateOnly> GetOccurrences();
}
