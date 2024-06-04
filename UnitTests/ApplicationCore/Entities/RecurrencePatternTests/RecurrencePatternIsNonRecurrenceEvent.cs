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
    public void Should_ReturnTrue_When_EventIsNonRecurrenceEvent()
    {
        bool result = _recurrencePattern.IsNonRecurrenceEvent();

        Assert.True(result);
    }

    [Theory]
    [InlineData(Frequency.Daily)]
    [InlineData(Frequency.Monthly)]
    [InlineData(Frequency.Weekly)]
    [InlineData(Frequency.Yearly)]
    public void Should_ReturnFalse_When_EventIsNotNonRecurrenceEvent(Frequency frequency)
    {
        _recurrencePattern.Frequency = frequency;

        bool result = _recurrencePattern.IsNonRecurrenceEvent();

        Assert.False(result);
    }
}
