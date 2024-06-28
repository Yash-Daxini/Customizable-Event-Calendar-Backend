using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Builders.EntityBuilder;

public class WeeklyRecurrencePatternBuilder
{
    private readonly WeeklyRecurrencePattern _recurrencePattern = new();

    public WeeklyRecurrencePatternBuilder()
    {
        _recurrencePattern.Frequency = Frequency.Weekly;
    }

    public WeeklyRecurrencePatternBuilder WithStartDate(DateOnly startDate)
    {
        _recurrencePattern.StartDate = startDate;
        return this;
    }

    public WeeklyRecurrencePatternBuilder WithEndDate(DateOnly endDate)
    {
        _recurrencePattern.EndDate = endDate;
        return this;
    }

    public WeeklyRecurrencePatternBuilder WithInterval(int interval)
    {
        _recurrencePattern.Interval = interval;
        return this;
    }

    public WeeklyRecurrencePatternBuilder WithByWeekDay(List<int>? byWeekDay)
    {
        _recurrencePattern.ByWeekDay = byWeekDay;
        return this;
    }

    public WeeklyRecurrencePattern Build() => _recurrencePattern;
}
