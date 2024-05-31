using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternIsNonRecurrenceEvent
{
    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternIsNonRecurrenceEvent()
    {
        _recurrencePattern = new RecurrencePattern()
        {
            Frequency = Frequency.None,
        };
    }

    [Fact]
    public void ReturnTrueIfFrequencyIsNonRecurrenceEvent()
    {
        bool result = _recurrencePattern.IsNonRecurrenceEvent();

        Assert.True(result);
    }

    [Theory]
    [InlineData(Frequency.Daily)]
    [InlineData(Frequency.Monthly)]
    [InlineData(Frequency.Weekly)]
    [InlineData(Frequency.Yearly)]
    public void ReturnFalseIfFrequencyIsNonRecurrenceEvent(Frequency frequency)
    {
        _recurrencePattern.Frequency = frequency;

        bool result = _recurrencePattern.IsNonRecurrenceEvent();

        Assert.False(result);
    }
}
