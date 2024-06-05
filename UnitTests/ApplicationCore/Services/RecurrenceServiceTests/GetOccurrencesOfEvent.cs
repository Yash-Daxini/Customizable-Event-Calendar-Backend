using Core.Entities;
using Core.Exceptions;
using Core.Services;

namespace UnitTests.ApplicationCore.Services.RecurrenceServiceTests;

public class GetOccurrencesOfEvent
{
    private readonly RecurrenceService _recurrenceService;
    public GetOccurrencesOfEvent()
    {
        _recurrenceService = new();
    }

    [Fact]
    public void Should_ReturnSingleOccurrence_When_ItIsNonRecurringEvent()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 5, 31),
            Frequency = Core.Entities.Enums.Frequency.None,
            WeekOrder = null,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = null,
            Interval = 1
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEvent()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 6, 20),
            Frequency = Core.Entities.Enums.Frequency.Daily,
            WeekOrder = null,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = null,
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31), new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 4), new DateOnly(2024, 6, 6),
                                         new DateOnly(2024, 6, 8),new DateOnly(2024, 6, 10),new DateOnly(2024, 6, 12),new DateOnly(2024, 6, 14),
                                         new DateOnly(2024, 6, 16),new DateOnly(2024, 6, 18),new DateOnly(2024, 6, 20)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsDailyRecurringEventWithWeekDays()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 6, 20),
            Frequency = Core.Entities.Enums.Frequency.Daily,
            WeekOrder = null,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = [6, 7],
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 6, 2), new DateOnly(2024, 6, 8), new DateOnly(2024, 6, 16)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsWeeklyRecurringEvent()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 6, 20),
            Frequency = Core.Entities.Enums.Frequency.Weekly,
            WeekOrder = null,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = [6, 7],
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 6, 1), new DateOnly(2024, 6, 2),
                                         new DateOnly(2024, 6, 15), new DateOnly(2024, 6, 16)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsMonthlyRecurringEventUsingMonthDayForLastDayOfMonth()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 10, 20),
            Frequency = Core.Entities.Enums.Frequency.Monthly,
            WeekOrder = null,
            ByMonth = null,
            ByMonthDay = 31,
            ByWeekDay = null,
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 31), new DateOnly(2024, 7, 31), new DateOnly(2024, 9, 30)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Theory]
    [InlineData(25)]
    [InlineData(21)]
    [InlineData(18)]
    [InlineData(1)]
    public void Should_ReturnListOfOccurrences_When_ItIsMonthlyRecurringEventUsingMonthDay(int monthDay)
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 10, 20),
            Frequency = Core.Entities.Enums.Frequency.Monthly,
            WeekOrder = null,
            ByMonth = null,
            ByMonthDay = monthDay,
            ByWeekDay = null,
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 5, monthDay), new DateOnly(2024, 7, monthDay), new DateOnly(2024, 9, monthDay)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsMonthlyRecurringEventUsingWeekOrder()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2024, 10, 20),
            Frequency = Core.Entities.Enums.Frequency.Monthly,
            WeekOrder = 5,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = [7],
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 5, 26), new DateOnly(2024, 7, 28), new DateOnly(2024, 9, 29)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsYearlyRecurringEventUsingMonthDayForLastDayOfMonth()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2025, 5, 20),
            Frequency = Core.Entities.Enums.Frequency.Yearly,
            WeekOrder = null,
            ByMonth = 9,
            ByMonthDay = 31,
            ByWeekDay = null,
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 9, 30)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }


    [Theory]
    [InlineData(25)]
    [InlineData(21)]
    [InlineData(19)]
    [InlineData(1)]
    [InlineData(5)]
    public void Should_ReturnListOfOccurrences_When_ItIsYearlyRecurringEventUsingMonthDay(int monthDay)
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2025, 5, 20),
            Frequency = Core.Entities.Enums.Frequency.Yearly,
            WeekOrder = null,
            ByMonth = 9,
            ByMonthDay = monthDay,
            ByWeekDay = null,
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 9, monthDay)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ReturnListOfOccurrences_When_ItIsYearlyRecurringEventUsingWeekOrder()
    {
        RecurrencePattern recurrencePattern = new()
        {
            StartDate = new DateOnly(2024, 5, 31),
            EndDate = new DateOnly(2025, 5, 20),
            Frequency = Core.Entities.Enums.Frequency.Yearly,
            WeekOrder = 5,
            ByMonth = 9,
            ByMonthDay = null,
            ByWeekDay = [7],
            Interval = 2
        };

        List<DateOnly> expectedOutput = [new DateOnly(2024, 9, 29)];

        List<DateOnly> actualOutput = _recurrenceService.GetOccurrencesOfEvent(recurrencePattern);

        Assert.Equal(expectedOutput, actualOutput);
    }

    [Fact]
    public void Should_ThrowException_When_RecurrencePatternIsNull()
    {
        Assert.Throws<InvalidRecurrencePatternException>(() =>
        {
            _recurrenceService.GetOccurrencesOfEvent(null);
        });
    }
}
