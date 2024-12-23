﻿using FluentAssertions;
using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class DurationDtoTest
{
    private readonly DurationDtoValidator _durationDtoValidator;

    public DurationDtoTest()
    {
        _durationDtoValidator = new();
    }

    [Theory]
    [InlineData(-1, -1)]
    [InlineData(-1, 0)]
    [InlineData(23, 24)]
    [InlineData(0, 24)]
    [InlineData(-1, 24)]
    [InlineData(2, 1)]
    [InlineData(23, 0)]
    [InlineData(23, 22)]
    [InlineData(23, 23)]
    [InlineData(21, 21)]
    public void Should_Return_False_When_InvalidDuration(int startHour, int endHour)
    {
        DurationDto durationDto = new DurationDto()
        {
            StartHour = startHour,
            EndHour = endHour
        };

        bool result = _durationDtoValidator.Validate(durationDto).IsValid;

        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(11, 12)]
    [InlineData(0, 1)]
    [InlineData(22, 23)]
    public void Should_Return_True_When_ValidDuration(int startHour, int endHour)
    {
        DurationDto durationDto = new()
        {
            StartHour = startHour,
            EndHour = endHour
        };

        var result = _durationDtoValidator.Validate(durationDto);

        result.IsValid.Should().BeTrue();
    }
}
