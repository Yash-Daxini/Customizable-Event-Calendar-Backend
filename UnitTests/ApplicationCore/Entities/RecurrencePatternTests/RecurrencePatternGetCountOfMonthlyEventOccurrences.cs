using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternGetCountOfMonthlyEventOccurrences
{

    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternGetCountOfMonthlyEventOccurrences()
    {
        _recurrencePattern = new RecurrencePattern();
    }

    [Theory]
    [InlineData(2024, 2024, 5, 6, 1)]
    [InlineData(2024, 2025, 2, 2, 2)]
    [InlineData(2024, 2026, 2, 8, 3)]
    [InlineData(2024, 2028, 1, 6, 4)]
    public void GetCountOfMonthlyOccurrences(int startDateYear, int endDateYear, int startDateMonth, int endDateMonth, int interval)
    {
        _recurrencePattern.StartDate = new DateOnly(startDateYear, startDateMonth, 1);
        _recurrencePattern.EndDate = new DateOnly(endDateYear, endDateMonth, 1);
        _recurrencePattern.Interval = interval;

        int expectedResult = _recurrencePattern.GetCountOfMonthlyEventOccurrences();

        int actualResult = ((endDateYear - startDateYear) * 12 + (endDateMonth - startDateMonth)) / interval + 1;

        Assert.Equal(expectedResult, actualResult);
    }
}
