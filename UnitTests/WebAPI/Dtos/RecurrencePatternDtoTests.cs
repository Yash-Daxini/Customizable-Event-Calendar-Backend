using FluentAssertions;
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

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_StartDateAndEndDateAreEmpty()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnFalse_When_StartDateGreaterThanEndDate()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 2),
            EndDate = new DateOnly(2024, 1, 1),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("daily")]
    [InlineData("weekly")]
    [InlineData("monthly")]
    [InlineData("yearly")]
    [InlineData("none")]
    [InlineData("")]
    [InlineData(null)]
    public void Should_ReturnFalse_When_InvalidFrequency(string frequency)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = frequency,
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData("Daily")]
    [InlineData("Weekly")]
    [InlineData("Monthly")]
    [InlineData("Yearly")]
    [InlineData("None")]
    public void Should_ReturnTrue_When_ValidFrequency(string frequency)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = frequency,
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(13)]
    [InlineData(0)]
    public void Should_ReturnFalse_When_InValidByMonth(int month)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = month,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(5)]
    public void Should_ReturnTrue_When_ValidByMonth(int month)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = month,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(32)]
    [InlineData(0)]
    public void Should_ReturnFalse_When_InValidByMonthDay(int monthDay)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = monthDay,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(31)]
    [InlineData(29)]
    public void Should_ReturnTrue_When_ValidByMonthDay(int monthDay)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = monthDay,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnFalse_When_InValidByWeekDay()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 41, 82],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidByWeekDay()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(0)]
    public void Should_ReturnFalse_When_InValidInterval(int interval)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 41, 82],
            Interval = interval,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Should_ReturnTrue_When_ValidInterval(int interval)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = interval,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(0)]
    public void Should_ReturnFalse_When_InValidWeekOrder(int weekOrder)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 41, 82],
            Interval = 1,
            WeekOrder = weekOrder,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    public void Should_ReturnTrue_When_ValidWeekOrder(int weekOrder)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = weekOrder,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidRecurrencePatternDto()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 2, 2),
            Frequency = "None",
            Interval = 1,
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = null,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }
}
