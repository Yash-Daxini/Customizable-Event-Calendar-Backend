
namespace Core.Entities;

public class SingleInstanceRecurrencePattern : RecurrencePattern
{
    public override List<DateOnly> GetOccurrences()
    {
        return [StartDate];
    }

    public override int GetOccurrencesCount()
    {
        return 1;
    }
}
