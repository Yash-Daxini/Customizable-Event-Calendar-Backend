using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternIsMonthDayNull
{
    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternIsMonthDayNull()
    {
        _recurrencePattern = new RecurrencePattern()
        {
            ByMonthDay = null,
        };
    }

    [Fact]
    public void Should_ReturnTrue_When_MonthDayIsNull()
    {
        bool result = _recurrencePattern.IsMonthDayNull();

        Assert.True(result);
    }

    [Fact]
    public void Should_ReturnFalse_When_MonthDayIsNotNull()
    {
        _recurrencePattern.ByMonthDay = 5;

        bool result = _recurrencePattern.IsMonthDayNull();

        Assert.False(result);
    }
}
