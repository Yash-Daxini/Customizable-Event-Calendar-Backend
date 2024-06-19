using Core.Entities.Enums;
using Core.Entities.RecurrecePattern;
using UnitTests.Builders;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternGetOccurrencesCount
{
    private RecurrencePattern _recurrencePattern;

    [Theory]
    [InlineData(2024, 2024, 5, 6, 1, 2)]
    [InlineData(2024, 2025, 2, 2, 2, 7)]
    [InlineData(2024, 2026, 2, 8, 3, 11)]
    [InlineData(2024, 2028, 1, 6, 4, 14)]
    [InlineData(2024, 2028, 1, 6, 5, 11)]
    [InlineData(2024, 2028, 1, 6, 0, 0)]
    [InlineData(2024, 2028, 1, 6, -5, 0)]
    public void Should_ReturnOccurrencesCountForMonthlyPattern_When_FrequencyIsMonthly(int startDateYear, int endDateYear, int startDateMonth, int endDateMonth, int interval, int expectedResult)
    {
        _recurrencePattern = new MonthlyRecurrencePatternBuilder()
                             .WithStartDate(new DateOnly(startDateYear, startDateMonth, 1))
                             .WithEndDate(new DateOnly(endDateYear, endDateMonth, 1))
                             .WithInterval(interval)
                             .WithFrequency()
                             .Build();

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData(2024, 2024, 1, 1)]
    [InlineData(2024, 2025, 2, 1)]
    [InlineData(2024, 2028, 3, 2)]
    [InlineData(2024, 2028, 4, 2)]
    [InlineData(2024, 2028, 5, 1)]
    [InlineData(2024, 2028, 1, 5)]
    [InlineData(2024, 2030, 2, 4)]
    [InlineData(2024, 2030, 3, 3)]
    [InlineData(2024, 2030, 4, 2)]
    [InlineData(2024, 2030, 5, 2)]
    [InlineData(2024, 2030, 0, 0)]
    [InlineData(2024, 2030, -4, 0)]
    public void Should_ReturnOccurrencesCountForYearlyPattern_When_FrequencyIsYearly(int startDateYear, int endDateYear, int interval, int expectedResult)
    {
        _recurrencePattern = new YearlyRecurrencePatternBuilder()
                             .WithStartDate(new DateOnly(startDateYear, 1, 1))
                             .WithEndDate(new DateOnly(endDateYear, 1, 1))
                             .WithInterval(interval)
                             .WithFrequency()
                             .Build();

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("1-1-2024", "4-4-2024", 1, 14)]
    [InlineData("4-2-2024", "1-8-2025", 2, 39)]
    [InlineData("2-3-2024", "15-1-2028", 3, 68)]
    [InlineData("1-4-2024", "2-10-2028", 4, 59)]
    [InlineData("5-5-2024", "1-5-2028", 5, 42)]
    [InlineData("5-5-2024", "1-5-2028", 0, 0)]
    [InlineData("5-5-2024", "1-5-2028", -3, 0)]
    public void Should_ReturnOccurrencesCountForWeeklyPattern_When_FrequencyIsWeekly(string startDate,
                                                                                     string endDate,
                                                                                     int interval,
                                                                                     int expectedResult)
    {
        _recurrencePattern = new WeeklyRecurrencePatternBuilder()
                             .WithStartDate(DateOnly.Parse(startDate))
                             .WithEndDate(DateOnly.Parse(endDate))
                             .WithInterval(interval)
                             .WithFrequency()
                             .Build();

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("1-1-2024", "4-4-2024", 1, 95)]
    [InlineData("4-2-2024", "1-8-2025", 2, 273)]
    [InlineData("2-3-2024", "15-1-2028", 3, 472)]
    [InlineData("1-4-2024", "2-10-2028", 4, 412)]
    [InlineData("5-5-2024", "1-5-2028", 5, 292)]
    [InlineData("5-5-2024", "1-5-2028", 0, 0)]
    [InlineData("5-5-2024", "1-5-2028", -2, 0)]
    public void Should_ReturnOccurrencesCountForDailyPattern_When_FrequencyIsDaily(string startDate,
                                                                                   string endDate,
                                                                                   int interval,
                                                                                   int expectedResult)
    {
        _recurrencePattern = new DailyRecurrencePatternBuilder()
                             .WithStartDate(DateOnly.Parse(startDate))
                             .WithEndDate(DateOnly.Parse(endDate))
                             .WithInterval(interval)
                             .WithFrequency()
                             .Build();

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Should_ReturnOccurrencesCountAsZero_When_FrequencyIsNone()
    {
        _recurrencePattern = new SingleInstanceRecurrencePatternBuilder()
                             .WithStartDate(new DateOnly(2024, 4, 1))
                             .WithEndDate(new DateOnly(2024, 4, 1))
                             .WithFrequency()
                             .Build();

        int expectedResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(1, expectedResult);
    }
}
