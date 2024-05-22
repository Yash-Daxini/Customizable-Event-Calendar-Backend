using Core.Domain.Enums;

namespace Core.Domain;

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
}
