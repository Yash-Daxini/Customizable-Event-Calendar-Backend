using Core.Entities.RecurrecePattern;
using FluentAssertions;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class YearlyRecurrencePatternGetOccurrences
{
    [Fact]
    public void Should_Return_ListOfOccurrences_When_ItIsYearlyRecurringEventUsingMonthDayForLastDayOfMonth()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(31)
                                              .WithByMonth(9)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> expectedResult = [new DateOnly(2024, 9, 30)];

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }


    [Theory]
    [InlineData(25)]
    [InlineData(21)]
    [InlineData(19)]
    [InlineData(1)]
    [InlineData(5)]
    public void Should_Return_ListOfOccurrences_When_ItIsYearlyRecurringEventUsingMonthDay(int monthDay)
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(monthDay)
                                              .WithByMonth(9)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> expectedResult = [new DateOnly(2024, 9, monthDay)];

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingMonthDayWithNullMonthDayWeekDayAndWeekOrder()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(null)
                                              .WithByMonth(9)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingMonthDayWithNullMonthWeekDayAndWeekOrder()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(1)
                                              .WithByMonth(null)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingMonthDayWithNullMonthAndNullMonthDay()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(null)
                                              .WithByMonth(null)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(32)]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingMonthDayWithInvalidMonthDay(int monthDay)
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(monthDay)
                                              .WithByMonth(1)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(13)]
    [InlineData(null)]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingMonthDayWithInvalidMonth(int month)
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(5)
                                              .WithByMonth(month)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_ListOfOccurrences_When_ItIsYearlyRecurringEventUsingWeekOrder()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(5)
                                              .WithByMonthDay(null)
                                              .WithByMonth(9)
                                              .WithByWeekDay([7])
                                              .Build();

        List<DateOnly> expectedResult = [new DateOnly(2024, 9, 29)];

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingWeekOrderWithNullWeekOrder()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(null)
                                              .WithByMonth(9)
                                              .WithByWeekDay([7])
                                              .Build();

        List<DateOnly> expectedResult = [];

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingWeekOrderWithZeroValueOfWeekOrder()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(0)
                                              .WithByMonthDay(null)
                                              .WithByMonth(9)
                                              .WithByWeekDay([7])
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_ListWithLastOccurrence_When_ItIsYearlyRecurringEventUsingWeekOrderWithValueOfWeekOrderIsGreaterThan5()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(10)
                                              .WithByMonthDay(null)
                                              .WithByMonth(9)
                                              .WithByWeekDay([7])
                                              .Build();

        List<DateOnly> expectedResult = [new DateOnly(2024, 9, 29)];

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEquivalentTo(expectedResult);
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingWeekOrderWithNullWeekDay()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(5)
                                              .WithByMonthDay(null)
                                              .WithByMonth(9)
                                              .WithByWeekDay(null)
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingWeekOrderWithNullByMonth()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(5)
                                              .WithByMonthDay(null)
                                              .WithByMonth(null)
                                              .WithByWeekDay([7])
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }

    [Fact]
    public void Should_Return_EmptyList_When_ItIsYearlyRecurringEventUsingWeekOrderWithWeekOrderAndByMonthIsNull()
    {
        RecurrencePattern recurrencePattern = new YearlyRecurrencePatternBuilder()
                                              .WithStartDate(new DateOnly(2024, 5, 31))
                                              .WithEndDate(new DateOnly(2025, 5, 20))
                                              .WithInterval(2)
                                              .WithWeekOrder(null)
                                              .WithByMonthDay(null)
                                              .WithByMonth(null)
                                              .WithByWeekDay([7])
                                              .Build();

        List<DateOnly> actualResult = recurrencePattern.GetOccurrences();

        actualResult.Should().BeEmpty();
    }
}
