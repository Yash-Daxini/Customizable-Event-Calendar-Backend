using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;

namespace UnitTests.Builders.EntityBuilder;

public class YearlyRecurrencePatternBuilder
{
    private readonly YearlyRecurrencePattern _recurrencePattern = new();

    public YearlyRecurrencePatternBuilder()
    {
        _recurrencePattern.Frequency = Frequency.Yearly;
    }

    public YearlyRecurrencePatternBuilder WithStartDate(DateOnly startDate)
    {
        _recurrencePattern.StartDate = startDate;
        return this;
    }

    public YearlyRecurrencePatternBuilder WithEndDate(DateOnly endDate)
    {
        _recurrencePattern.EndDate = endDate;
        return this;
    }

    public YearlyRecurrencePatternBuilder WithInterval(int interval)
    {
        _recurrencePattern.Interval = interval;
        return this;
    }

    public YearlyRecurrencePatternBuilder WithByWeekDay(List<int>? byWeekDay)
    {
        _recurrencePattern.ByWeekDay = byWeekDay;
        return this;
    }

    public YearlyRecurrencePatternBuilder WithByMonthDay(int? byMonthDay)
    {
        _recurrencePattern.ByMonthDay = byMonthDay;
        return this;
    }

    public YearlyRecurrencePatternBuilder WithWeekOrder(int? weekOrder)
    {
        _recurrencePattern.WeekOrder = weekOrder;
        return this;
    }

    public YearlyRecurrencePatternBuilder WithByMonth(int? byMonth)
    {
        _recurrencePattern.ByMonth = byMonth;
        return this;
    }

    public YearlyRecurrencePattern Build() => _recurrencePattern;
}
