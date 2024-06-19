using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Builders;

public class MonthlyRecurrencePatternBuilder
{
    private readonly MonthlyRecurrencePattern _recurrencePattern = new();

    public MonthlyRecurrencePatternBuilder WithStartDate(DateOnly startDate)
    {
        _recurrencePattern.StartDate = startDate;
        return this;
    }

    public MonthlyRecurrencePatternBuilder WithEndDate(DateOnly endDate)
    {
        _recurrencePattern.EndDate = endDate;
        return this;
    }

    public MonthlyRecurrencePatternBuilder WithInterval(int interval)
    {
        _recurrencePattern.Interval = interval;
        return this;
    }

    public MonthlyRecurrencePatternBuilder WithFrequency()
    {
        _recurrencePattern.Frequency = Frequency.Monthly;
        return this;
    }

    public MonthlyRecurrencePatternBuilder WithByWeekDay(List<int>? byWeekDay)
    {
        _recurrencePattern.ByWeekDay = byWeekDay;
        return this;
    }
    
    public MonthlyRecurrencePatternBuilder WithByMonthDay(int? byMonthDay)
    {
        _recurrencePattern.ByMonthDay = byMonthDay;
        return this;
    }

    public MonthlyRecurrencePatternBuilder WithWeekOrder(int? weekOrder)
    {
        _recurrencePattern.WeekOrder = weekOrder;
        return this;
    }

    public MonthlyRecurrencePattern Build() => _recurrencePattern;
}
