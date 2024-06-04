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

    public override bool Equals(object? obj)
    {
        if (obj is RecurrencePattern recurrencePattern)
        {
            bool result = this.StartDate.Equals(recurrencePattern.StartDate)
                && this.EndDate.Equals(recurrencePattern.EndDate)
                && this.Frequency.Equals(recurrencePattern.Frequency)
                && this.Interval == recurrencePattern.Interval
                && this.ByMonth == recurrencePattern.ByMonth
                && this.ByMonthDay == recurrencePattern.ByMonthDay
                && this.WeekOrder == recurrencePattern.WeekOrder;

            if (this.ByWeekDay is not null && recurrencePattern.ByWeekDay is not null)
            {
                result &= this.ByWeekDay.Count == recurrencePattern.ByWeekDay.Count;

                foreach (var day in ByWeekDay)
                {
                    result &= recurrencePattern.ByWeekDay.Contains(day);
                }
            }
            else
            {
                result &= this.ByWeekDay is null && recurrencePattern.ByWeekDay is null;
            }

            return result;

        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(this.StartDate,
                                this.EndDate,
                                this.Frequency,
                                this.Interval,
                                this.ByWeekDay,
                                this.ByMonth,
                                this.ByMonthDay,
                                this.WeekOrder);
    }
}
