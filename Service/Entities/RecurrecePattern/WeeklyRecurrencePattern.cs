using Core.Extensions;

namespace Core.Entities.RecurrecePattern;

public class WeeklyRecurrencePattern : RecurrencePattern
{
    public override List<DateOnly> GetOccurrences()
    {
        List<DateOnly> occurrences = [];

        List<int> weekDays = [.. ByWeekDay ?? ([])];

        DateOnly startDateOfWeek = StartDate.GetStartDateOfWeek();

        foreach (var weekDay in weekDays)
        {
            occurrences = [.. occurrences.Concat(GetOccurrencesOfWeekDay(startDateOfWeek, weekDay))];
        }

        occurrences.Sort();

        return occurrences;
    }

    public override int GetOccurrencesCount()
    {
        if (Interval <= 0)
            return 0;

        TimeSpan difference = EndDate.ConvertToDateTime()
                            - StartDate.ConvertToDateTime();

        return (int)difference.TotalDays / (7 * Interval) + 1;
    }

    private List<DateOnly> GetOccurrencesOfWeekDay(DateOnly startDateOfWeek, int weekDay)
    {
        DateOnly startDateOfEvent = StartDate;
        DateOnly endDateOfEvent = EndDate;

        DateOnly startDateForSpecificWeekday = startDateOfWeek.AddDays(weekDay - 1);

        int interval = Interval;

        int totalOccurrences = GetOccurrencesCount();

        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => startDateForSpecificWeekday.AddDays(7 * weekOffset * interval))
                            .Where(date => date.IsDateInRange(startDateOfEvent,endDateOfEvent))];
    }
}
