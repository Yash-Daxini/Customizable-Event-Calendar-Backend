using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Builders;

public class DailyRecurrencePatternBuilder
{
    private readonly DailyRecurrencePattern _recurrencePattern = new();

    public DailyRecurrencePatternBuilder WithStartDate(DateOnly startDate)
    {
        _recurrencePattern.StartDate = startDate;
        return this;
    }

    public DailyRecurrencePatternBuilder WithEndDate(DateOnly endDate)
    {
        _recurrencePattern.EndDate = endDate;
        return this;
    }

    public DailyRecurrencePatternBuilder WithInterval(int interval)
    {
        _recurrencePattern.Interval = interval;
        return this;
    }

    public DailyRecurrencePatternBuilder WithFrequency()
    {
        _recurrencePattern.Frequency = Frequency.Daily;
        return this;
    }

    public DailyRecurrencePatternBuilder WithByWeekDay(List<int>? byWeekDay)
    {
        _recurrencePattern.ByWeekDay = byWeekDay;
        return this;
    }

    public DailyRecurrencePattern Build() => _recurrencePattern;
}
