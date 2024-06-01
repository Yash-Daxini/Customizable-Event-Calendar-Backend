using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternMakeNonRecurringEvent
{

    private readonly RecurrencePattern _nonRecurringRecurrencePattern;

    public RecurrencePatternMakeNonRecurringEvent()
    {
        _nonRecurringRecurrencePattern = new()
        {
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            Frequency = Frequency.None,
            Interval = 1,
            ByWeekDay = null,
            ByMonthDay = null,
            ByMonth = null,
            WeekOrder = null,
        };
    }

    [Fact]
    public void MakeNonRecurringEventIfEventIsDailyRecurringEvent()
    {
        RecurrencePattern recurrencePattern = new()
        {
            Frequency = Frequency.Daily,
            Interval = 1,
            ByWeekDay = [1, 2, 3, 4, 5],
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
        };

        recurrencePattern.MakeNonRecurringEvent();

        Assert.Equivalent(recurrencePattern, _nonRecurringRecurrencePattern);
    }

    [Fact]
    public void MakeNonRecurringEventIfEventIsMonthlyRecurringEventUsingMonthDay()
    {
        RecurrencePattern recurrencePattern = new()
        {
            Frequency = Frequency.Monthly,
            Interval = 2,
            ByWeekDay = null,
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            ByMonth = null,
            ByMonthDay = 5,
            WeekOrder = null
        };

        recurrencePattern.MakeNonRecurringEvent();

        Assert.Equivalent(recurrencePattern, _nonRecurringRecurrencePattern);
    }

    [Fact]
    public void MakeNonRecurringEventIfEventIsMonthlyRecurringEventUsingWeekOrder()
    {
        RecurrencePattern recurrencePattern = new()
        {
            Frequency = Frequency.Monthly,
            Interval = 2,
            ByWeekDay = [2],
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            ByMonth = null,
            ByMonthDay = null,
            WeekOrder = 2
        };

        recurrencePattern.MakeNonRecurringEvent();

        Assert.Equivalent(recurrencePattern, _nonRecurringRecurrencePattern);
    }

    [Fact]
    public void MakeNonRecurringEventIfEventIsYearlyRecurringEventUsingMonthDay()
    {
        RecurrencePattern recurrencePattern = new()
        {
            Frequency = Frequency.Yearly,
            Interval = 2,
            ByWeekDay = null,
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            ByMonth = 2,
            ByMonthDay = 5,
            WeekOrder = null
        };

        recurrencePattern.MakeNonRecurringEvent();

        Assert.Equivalent(recurrencePattern, _nonRecurringRecurrencePattern);
    }

    [Fact]
    public void MakeNonRecurringEventIfEventIsYearlyRecurringEventUsingWeekOrder()
    {
        RecurrencePattern recurrencePattern = new()
        {
            Frequency = Frequency.Yearly,
            Interval = 2,
            ByWeekDay = [2],
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            ByMonth = null,
            ByMonthDay = null,
            WeekOrder = 2
        };

        recurrencePattern.MakeNonRecurringEvent();

        Assert.Equivalent(recurrencePattern, _nonRecurringRecurrencePattern);
    }

    [Fact]
    public void MakeNonRecurringEventIfEventIsNonRecurringEvent()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            Frequency = Frequency.None,
            Interval = 1,
            ByWeekDay = null,
            ByMonthDay = null,
            ByMonth = null,
            WeekOrder = null,
        };

        recurrencePattern.MakeNonRecurringEvent();

        Assert.Equivalent(recurrencePattern, _nonRecurringRecurrencePattern);
    }
}
