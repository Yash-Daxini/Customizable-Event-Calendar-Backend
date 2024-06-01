using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternGetYearlyOccurrencesCount
{
    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternGetYearlyOccurrencesCount()
    {
        _recurrencePattern = new RecurrencePattern();
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
    public void GetYearlyOccurrencesCountIfValidDates(int startDateYear, int endDateYear, int interval, int actualResult)
    {
        _recurrencePattern.StartDate = new DateOnly(startDateYear, 1, 1);
        _recurrencePattern.EndDate = new DateOnly(endDateYear, 1, 1);
        _recurrencePattern.Interval = interval;

        int expectedResult = _recurrencePattern.GetYearlyOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }
}
