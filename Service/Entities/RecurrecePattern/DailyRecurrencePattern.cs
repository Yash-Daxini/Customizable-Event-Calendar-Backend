﻿using Core.Extensions;

namespace Core.Entities.RecurrecePattern;

public class DailyRecurrencePattern : RecurrencePattern
{
    public override List<DateOnly> GetOccurrences()
    {
        List<int> days = [.. ByWeekDay ?? ([])];

        if (Interval == 0)
            return [];

        int totalOccurrences = GetOccurrencesCount();

        return [..Enumerable.Range(0, totalOccurrences)
                            .Select(weekOffset => StartDate
                                                  .AddDays(weekOffset * Interval))
                            .Where(date => IsValidDateForDailyEvent(date, days))];
    }

    public override int GetOccurrencesCount()
    {
        if (Interval <= 0)
            return 0;

        TimeSpan difference = EndDate.ConvertToDateTime()
                            - StartDate.ConvertToDateTime();

        return (int)difference.TotalDays / Interval + 1;
    }

    private bool IsValidDateForDailyEvent(DateOnly date,
                                          List<int> days)
    {
        return days.Count == 0 || days.Contains(date.GetDayNumberFromWeekDay());
    }
}
