using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class RecurrencePatternDtoTests
{
    RecurrencePatternDtoValidator _validator;

    public RecurrencePatternDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidRecurrencePatternDto()
    {
        RecurrencePatternDto recurrencePatternDto = new();

        var result = _validator.Validate(recurrencePatternDto);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidRecurrencePatternDto()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024,1,1),
            EndDate = new DateOnly(2024,2,2),
            Frequency = "None",
            Interval = 1,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = null,
            WeekOrder = null,   
        };

        var result = _validator.Validate(recurrencePatternDto);

        Assert.True(result.IsValid);
    }
}
