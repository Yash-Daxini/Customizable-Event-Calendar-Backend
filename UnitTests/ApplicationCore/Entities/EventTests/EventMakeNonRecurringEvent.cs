using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.EventTests;

public class EventMakeNonRecurringEvent
{
    private readonly Event _event;

    public EventMakeNonRecurringEvent()
    {
        _event = new()
        {
            RecurrencePattern = new()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Frequency.None,
                Interval = 1,
                ByWeekDay = null,
                ByMonthDay = null,
                ByMonth = null,
                WeekOrder = null,
            }
        };
    }

    [Fact]
    public void Should_MakeNonRecurringEvent_When_EventIsDailyRecurringEvent()
    {
        Event eventObj = new()
        {
            RecurrencePattern = new()
            {
                Frequency = Frequency.Daily,
                Interval = 1,
                ByWeekDay = [1, 2, 3, 4, 5],
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
            },
        };

        eventObj.MakeNonRecurringEvent();

        Assert.Equivalent(_event, eventObj);
    }

    [Fact]
    public void Should_MakeNonRecurringEvent_When_EventIsMonthlyRecurringEventUsingMonthDay()
    {
        Event eventObj = new()
        {
            RecurrencePattern = new()
            {
                Frequency = Frequency.Monthly,
                Interval = 2,
                ByWeekDay = null,
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                ByMonth = null,
                ByMonthDay = 5,
                WeekOrder = null
            }
        };

        eventObj.MakeNonRecurringEvent();

        Assert.Equivalent(_event, eventObj);
    }

    [Fact]
    public void Should_MakeNonRecurringEvent_When_EventIsMonthlyRecurringEventUsingWeekOrder()
    {
        Event eventObj = new()
        {
            RecurrencePattern = new()
            {
                Frequency = Frequency.Monthly,
                Interval = 2,
                ByWeekDay = [2],
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                ByMonth = null,
                ByMonthDay = null,
                WeekOrder = 2
            }
        };

        eventObj.MakeNonRecurringEvent();

        Assert.Equivalent(_event, eventObj);
    }

    [Fact]
    public void Should_MakeNonRecurringEvent_When_EventIsYearlyRecurringEventUsingMonthDay()
    {
        Event eventObj = new()
        {
            RecurrencePattern = new()
            {
                Frequency = Frequency.Yearly,
                Interval = 2,
                ByWeekDay = null,
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                ByMonth = 2,
                ByMonthDay = 5,
                WeekOrder = null
            }
        };

        eventObj.MakeNonRecurringEvent();

        Assert.Equivalent(_event, eventObj);
    }

    [Fact]
    public void Should_MakeNonRecurringEvent_When_EventIsYearlyRecurringEventUsingWeekOrder()
    {
        Event eventObj = new()
        {
            RecurrencePattern = new()
            {
                Frequency = Frequency.Yearly,
                Interval = 2,
                ByWeekDay = [2],
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                ByMonth = null,
                ByMonthDay = null,
                WeekOrder = 2
            }
        };

        eventObj.MakeNonRecurringEvent();

        Assert.Equivalent(_event, eventObj);
    }

    [Fact]
    public void Should_MakeNonRecurringEvent_When_EventIsNonRecurringEvent()
    {
        Event eventObj = new()
        {
            RecurrencePattern = new()
            {
                StartDate = new DateOnly(),
                EndDate = new DateOnly(),
                Frequency = Frequency.None,
                Interval = 1,
                ByWeekDay = null,
                ByMonthDay = null,
                ByMonth = null,
                WeekOrder = null,
            }
        };

        eventObj.MakeNonRecurringEvent();

        Assert.Equivalent(_event, eventObj);
    }
}
