using Core.Entities.Enums;
using Core.Entities;

namespace UnitTests.ApplicationCore.Entities.RecurrencePatternTests;

public class RecurrencePatternIsYearlyEvent
{
    private readonly RecurrencePattern _recurrencePattern;

    public RecurrencePatternIsYearlyEvent()
    {
        _recurrencePattern = new RecurrencePattern()
        {
            Frequency = Frequency.Yearly,
        };
    }

    [Fact]
    public void Should_ReturnTrue_When_FrequencyIsYearly()
    {
        bool result = _recurrencePattern.IsYearlyEvent();

        Assert.True(result);
    }

    [Theory]
    [InlineData(Frequency.Monthly)]
    [InlineData(Frequency.Daily)]
    [InlineData(Frequency.Weekly)]
    [InlineData(Frequency.None)]
    public void Should_ReturnFalse_When_FrequencyIsNotYearly(Frequency frequency)
    {
        _recurrencePattern.Frequency = frequency;

        bool result = _recurrencePattern.IsYearlyEvent();

        Assert.False(result);
    }
}
