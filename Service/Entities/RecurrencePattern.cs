using Core.Entities.Enums;

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

    public bool IsNonRecurrenceEvent() => this.Frequency == Frequency.None;

    public bool IsMonthDayNull() => this.ByMonthDay == null;

    public int GetMonthlyOccurrencesCount()
    {
        return ((EndDate.Year - StartDate.Year) * 12 + (EndDate.Month - StartDate.Month)) / Interval + 1;
    }

    public int GetYearlyOccurrencesCount()
    {
        return (EndDate.Year - StartDate.Year) / Interval + 1;
    }

    public void MakeNonRecurringEvent()
    {
        Frequency = Frequency.None;
        Interval = 1;
        ByWeekDay = null;
        ByMonthDay = null;
        ByMonth = null;
        WeekOrder = null;
    }
}
