using Core.Entities;
using Core.Entities.Enums;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternGetOccurrencesCount
{
    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternGetOccurrencesCount()
    {
        _recurrencePattern = new RecurrencePattern();
    }

    [Theory]
    [InlineData(2024, 2024, 5, 6, 1, 2)]
    [InlineData(2024, 2025, 2, 2, 2, 7)]
    [InlineData(2024, 2026, 2, 8, 3, 11)]
    [InlineData(2024, 2028, 1, 6, 4, 14)]
    [InlineData(2024, 2028, 1, 6, 5, 11)]
    public void Should_ReturnOccurrencesCountForMonthlyPattern_When_FrequencyIsMonthly(int startDateYear, int endDateYear, int startDateMonth, int endDateMonth, int interval, int expectedResult)
    {
        _recurrencePattern.StartDate = new DateOnly(startDateYear, startDateMonth, 1);
        _recurrencePattern.EndDate = new DateOnly(endDateYear, endDateMonth, 1);
        _recurrencePattern.Interval = interval;
        _recurrencePattern.Frequency = Frequency.Monthly;

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
    public void Should_ReturnOccurrencesCountForYearlyPattern_When_FrequencyIsYearly(int startDateYear, int endDateYear, int interval, int expectedResult)
    {
        _recurrencePattern.StartDate = new DateOnly(startDateYear, 1, 1);
        _recurrencePattern.EndDate = new DateOnly(endDateYear, 1, 1);
        _recurrencePattern.Interval = interval;
        _recurrencePattern.Frequency = Frequency.Yearly;

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("1-1-2024", "4-4-2024", 1, 14)]
    [InlineData("4-2-2024", "1-8-2025", 2, 39)]
    [InlineData("2-3-2024", "15-1-2028", 3, 68)]
    [InlineData("1-4-2024", "2-10-2028", 4, 59)]
    [InlineData("5-5-2024", "1-5-2028", 5, 42)]
    public void Should_ReturnOccurrencesCountForWeeklyPattern_When_FrequencyIsWeekly(string startDate,
                                                                                     string endDate,
                                                                                     int interval,
                                                                                     int expectedResult)
    {
        _recurrencePattern.StartDate = DateOnly.Parse(startDate);
        _recurrencePattern.EndDate = DateOnly.Parse(endDate);
        _recurrencePattern.Interval = interval;
        _recurrencePattern.Frequency = Frequency.Weekly;

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Theory]
    [InlineData("1-1-2024", "4-4-2024", 1, 95)]
    [InlineData("4-2-2024", "1-8-2025", 2, 273)]
    [InlineData("2-3-2024", "15-1-2028", 3, 472)]
    [InlineData("1-4-2024", "2-10-2028", 4, 412)]
    [InlineData("5-5-2024", "1-5-2028", 5, 292)]
    public void Should_ReturnOccurrencesCountForDailyPattern_When_FrequencyIsDaily(string startDate,
                                                                                   string endDate,
                                                                                   int interval,
                                                                                   int expectedResult)
    {
        _recurrencePattern.StartDate = DateOnly.Parse(startDate);
        _recurrencePattern.EndDate = DateOnly.Parse(endDate);
        _recurrencePattern.Interval = interval;
        _recurrencePattern.Frequency = Frequency.Daily;

        int actualResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }

    [Fact]
    public void Should_ReturnOccurrencesCountAsZero_When_FrequencyIsNone()
    {
        _recurrencePattern.Frequency = Frequency.None;

        int expectedResult = _recurrencePattern.GetOccurrencesCount();

        Assert.Equal(0,expectedResult);
    }
}
