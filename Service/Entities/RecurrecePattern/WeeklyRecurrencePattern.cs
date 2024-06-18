using Core.Extensions;

namespace Core.Entities.RecurrecePattern;

public class WeeklyRecurrencePattern : RecurrencePattern
{
    public override List<DateOnly> GetOccurrences()
    {
        List<DateOnly> occurrences = [];

        List<int> weekDays = [.. ByWeekDay ?? ([])];

        DateOnly startDateOfWeek = StartDate.GetStartDateOfWeek();

        foreach (var item in weekDays)
        {
            occurrences = [.. occurrences.Concat(GetOccurrencesOfWeekDay(startDateOfWeek, item))];
        }

        occurrences.Sort();

        return occurrences;
    }

    public override int GetOccurrencesCount()
    {
        TimeSpan difference = EndDate.ConvertToDateTime()
                            - StartDate.ConvertToDateTime();

        return (int)difference.TotalDays / (7 * Interval) + 1;
    }

    private List<DateOnly> GetOccurrencesOfWeekDay(DateOnly startDateOfWeek, int item)
    {
        DateOnly startDateOfEvent = StartDate;
        DateOnly endDateOfEvent = EndDate;

        DateOnly startDateForSpecificWeekday = startDateOfWeek.AddDays(item - 1);

        int interval = Interval;

        int totalOccurrences = GetOccurrencesCount();

        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => startDateForSpecificWeekday.AddDays(7 * weekOffset * interval))
                            .Where(date => date.IsDateInRange(startDateOfEvent,endDateOfEvent))];
    }
}
