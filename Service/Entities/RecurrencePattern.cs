using Core.Entities.Enums;
using Core.Extensions;

namespace Core.Entities;

public class RecurrencePattern
{
    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public Frequency Frequency { get; set; }

    public int Interval { get; set; }

    public List<int>? ByWeekDay { get; set; }

    public int? WeekOrder { get; set; }

    public int? ByMonthDay { get; set; }

    public int? ByMonth { get; set; }

    public bool IsDailyEvent() => this.Frequency == Frequency.Daily;

    public bool IsWeeklyEvent() => this.Frequency == Frequency.Weekly;

    public bool IsMonthlyEvent() => this.Frequency == Frequency.Monthly;

    public bool IsYearlyEvent() => this.Frequency == Frequency.Yearly;

    public bool IsNonRecurrenceEvent() => this.Frequency == Frequency.None;

    public bool IsMonthDayNull() => this.ByMonthDay == null;

    private int GetMonthlyOccurrencesCount()
    {
        return (((EndDate.Year - StartDate.Year) * 12 + (EndDate.Month - StartDate.Month)) / Interval) + 1;
    }

    private int GetYearlyOccurrencesCount()
    {
        return ((EndDate.Year - StartDate.Year) / Interval) + 1;
    }

    private int GetDailyOccurrencesCount()
    {
        TimeSpan difference = EndDate.ConvertToDateTime()
                            - StartDate.ConvertToDateTime();

        return ((int)difference.TotalDays / Interval) + 1;
    }

    private int GetWeeklyOccurrencesCount()
    {
        TimeSpan difference = EndDate.ConvertToDateTime()
                            - StartDate.ConvertToDateTime();

        return ((int)difference.TotalDays / (7 * Interval)) + 1;
    }

    public int GetOccurrencesCount()
    {
        return true switch
        {
            var _ when IsDailyEvent() => GetDailyOccurrencesCount(),
            var _ when IsWeeklyEvent() => GetWeeklyOccurrencesCount(),
            var _ when IsMonthlyEvent() => GetMonthlyOccurrencesCount(),
            var _ when IsYearlyEvent() => GetYearlyOccurrencesCount(),
            _ => 0,
        };
    }
}
