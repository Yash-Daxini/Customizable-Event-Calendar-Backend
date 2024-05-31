using Core.Entities.Enums;
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
    public void ReturnTrueIfMonthDayIsNull()
    {
        bool result = _recurrencePattern.IsMonthDayNull();

        Assert.True(result);
    }

    [Fact]
    public void ReturnFalseIfMonthDayIsNotNull()
    {
        _recurrencePattern.ByMonthDay = 5;

        bool result = _recurrencePattern.IsMonthDayNull();

        Assert.False(result);
    }
}
