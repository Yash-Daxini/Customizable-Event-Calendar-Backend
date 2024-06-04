using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternGetMonthlyOccurrencesCount
{

    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternGetMonthlyOccurrencesCount()
    {
        _recurrencePattern = new RecurrencePattern();
    }

    [Theory]
    [InlineData(2024, 2024, 5, 6, 1, 2)]
    [InlineData(2024, 2025, 2, 2, 2, 7)]
    [InlineData(2024, 2026, 2, 8, 3, 11)]
    [InlineData(2024, 2028, 1, 6, 4, 14)]
    [InlineData(2024, 2028, 1, 6, 5, 11)]
    public void Should_ReturnMonthlyOccurrencesCount_When_CallsTheMethod(int startDateYear, int endDateYear, int startDateMonth, int endDateMonth, int interval, int actualResult)
    {
        _recurrencePattern.StartDate = new DateOnly(startDateYear, startDateMonth, 1);
        _recurrencePattern.EndDate = new DateOnly(endDateYear, endDateMonth, 1);
        _recurrencePattern.Interval = interval;

        int expectedResult = _recurrencePattern.GetMonthlyOccurrencesCount();

        Assert.Equal(expectedResult, actualResult);
    }
}
