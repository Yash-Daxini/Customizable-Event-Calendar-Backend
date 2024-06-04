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
    public void Should_ReturnTrue_When_FrequencyIsMonthly()
    {
        bool result = _recurrencePattern.IsMonthlyEvent();

        Assert.True(result);
    }

    [Theory]
    [InlineData(Frequency.Yearly)]
    [InlineData(Frequency.Daily)]
    [InlineData(Frequency.Weekly)]
    [InlineData(Frequency.None)]
    public void Should_ReturnFalse_When_FrequencyIsNotMonthly(Frequency frequency)
    {
        _recurrencePattern.Frequency = frequency;

        bool result = _recurrencePattern.IsMonthlyEvent();

        Assert.False(result);
    }
}
