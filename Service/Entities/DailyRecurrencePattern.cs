
using Core.Extensions;

namespace Core.Entities;

public class DailyRecurrencePattern : RecurrencePattern
{
    public override List<DateOnly> GetOccurrences()
    {
        List<int> days = [.. ByWeekDay ?? ([])];

        DateOnly startDateOfEvent = StartDate;
        DateOnly endDateOfEvent = EndDate;
        int interval = Interval;

        int totalOccurrences = GetOccurrencesCount();

        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => startDateOfEvent.AddDays(weekOffset * interval))
                            .Where(date => IsValidDateForDailyEvent(date, days))];
    }

    public override int GetOccurrencesCount()
    {
        TimeSpan difference = EndDate.ConvertToDateTime()
                            - StartDate.ConvertToDateTime();

        return ((int)difference.TotalDays / Interval) + 1;
    }
    private bool IsValidDateForDailyEvent(DateOnly date,
                                          List<int> days)
    {
        return days.Count == 0 || days.Contains(date.GetDayNumberFromWeekDay());
    }
}
