using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class RecurrencePatternDtoTests
{
    private readonly RecurrencePatternDtoValidator _validator;

    public RecurrencePatternDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_Return_False_When_InvalidRecurrencePatternDto()
    {
        RecurrencePatternDto recurrencePatternDto = new();

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_StartDateAndEndDateAreEmpty()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(),
            EndDate = new DateOnly(),
            Frequency = "Daily",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_False_When_StartDateGreaterThanEndDate()
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
    public void Should_Return_False_When_InvalidFrequency(string frequency)
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

    [Fact]
    public void Should_Return_True_When_ValidFrequencyWithValidDailyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_InValidFrequencyWithValidDailyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = null,
            ByMonthDay = 1,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidFrequencyWithValidWeeklyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Weekly",
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = [1,2,3],
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_InValidFrequencyWithInValidWeeklyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Weekly",
            ByMonth = 1,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = 5,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Should_Return_True_When_ValidFrequencyWithValidMonthlyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Monthly",
            ByMonth = null,
            ByMonthDay = 1,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_InValidFrequencyWithInValidMonthlyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Monthly",
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
    public void Should_Return_True_When_ValidFrequencyWithValidYearlyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Yearly",
            ByMonth = 1,
            ByMonthDay = 31,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_InValidFrequencyWithInValidYearlyRecurrencePattern()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Yearly",
            ByMonth = -1,
            ByMonthDay = null,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(13)]
    [InlineData(0)]
    public void Should_Return_False_When_InValidByMonth(int month)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Yearly",
            ByMonth = month,
            ByMonthDay = 1,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(12)]
    [InlineData(5)]
    public void Should_Return_True_When_ValidByMonth(int month)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Yearly",
            ByMonth = month,
            ByMonthDay = null,
            ByWeekDay = null,
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
    public void Should_Return_False_When_InValidByMonthDay(int monthDay)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Monthly",
            ByMonth = 1,
            ByMonthDay = monthDay,
            ByWeekDay = null,
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
    public void Should_Return_True_When_ValidByMonthDay(int monthDay)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Monthly",
            ByMonth = null,
            ByMonthDay = monthDay,
            ByWeekDay = null,
            Interval = 1,
            WeekOrder = 1,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_False_When_InValidByWeekDay()
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
    public void Should_Return_True_When_ValidByWeekDay()
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(0)]
    public void Should_Return_False_When_InValidInterval(int interval)
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
    public void Should_Return_True_When_ValidInterval(int interval)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Daily",
            ByMonth = null,
            ByMonthDay = null,
            ByWeekDay = [1, 2, 3],
            Interval = interval,
            WeekOrder = null,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(6)]
    [InlineData(0)]
    public void Should_Return_False_When_InValidWeekOrder(int weekOrder)
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
    public void Should_Return_True_When_ValidWeekOrder(int weekOrder)
    {
        RecurrencePatternDto recurrencePatternDto = new()
        {
            StartDate = new DateOnly(2024, 1, 1),
            EndDate = new DateOnly(2024, 1, 2),
            Frequency = "Monthly",
            ByMonth = null,
            ByMonthDay = 1,
            ByWeekDay = [1, 2, 3],
            Interval = 1,
            WeekOrder = weekOrder,
        };

        var result = _validator.Validate(recurrencePatternDto);

        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Should_Return_True_When_ValidRecurrencePatternDto()
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
