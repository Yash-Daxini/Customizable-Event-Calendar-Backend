using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternIsMonthlyEvent
{
    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternIsMonthlyEvent()
    {
        _recurrencePattern = new RecurrencePattern()
        {
            Frequency = Frequency.Monthly,
        };
    }

    [Fact]
    public void ReturnTrueIfFrequencyIsMonthly()
    {
        bool result = _recurrencePattern.IsMonthlyEvent();

        Assert.True(result);
    }

    [Theory]
    [InlineData(Frequency.Yearly)]
    [InlineData(Frequency.Daily)]
    [InlineData(Frequency.Weekly)]
    [InlineData(Frequency.None)]
    public void ReturnFalseIfFrequencyIsMonthly(Frequency frequency)
    {
        _recurrencePattern.Frequency = frequency;

        bool result = _recurrencePattern.IsMonthlyEvent();

        Assert.False(result);
    }
}
