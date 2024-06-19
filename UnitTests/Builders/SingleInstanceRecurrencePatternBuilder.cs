using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Builders;

public class SingleInstanceRecurrencePatternBuilder
{
    private readonly SingleInstanceRecurrencePattern _recurrencePattern = new();

    public SingleInstanceRecurrencePatternBuilder WithStartDate(DateOnly startDate)
    {
        _recurrencePattern.StartDate = startDate;
        return this;
    }

    public SingleInstanceRecurrencePatternBuilder WithEndDate(DateOnly endDate)
    {
        _recurrencePattern.EndDate = endDate;
        return this;
    }

    public SingleInstanceRecurrencePatternBuilder WithInterval(int interval)
    {
        _recurrencePattern.Interval = interval;
        return this;
    }

    public SingleInstanceRecurrencePatternBuilder WithFrequency()
    {
        _recurrencePattern.Frequency = Frequency.None;
        return this;
    }

    public SingleInstanceRecurrencePatternBuilder WithByWeekDay()
    {
        _recurrencePattern.ByWeekDay = null;
        return this;
    }

    public SingleInstanceRecurrencePattern Build() => _recurrencePattern;
}
