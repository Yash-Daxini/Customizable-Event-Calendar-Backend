﻿using Core.Entities;
using Core.Exceptions;
using FluentAssertions;

namespace UnitTests.ApplicationCore.Entities.DurationTests;

public class DurationGetDurationInFormat
{
    [Theory]
    [InlineData(-5, 25)]
    [InlineData(-10, -25)]
    public void Should_Throw_Exception_When_InvalidStartHourAndInValidEndHour(int startHour, int endHour)
    {
        Action action = () =>
        {
            Duration duration = new(startHour, endHour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(-5, 23)]
    [InlineData(25, 23)]
    public void Should_Throw_Exception_When_InvalidStartHourAndValidEndHour(int startHour, int endHour)
    {
        Action action = () =>
        {
            Duration duration = new(startHour, endHour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(5, 25)]
    [InlineData(3, -4)]
    public void Should_Throw_Exception_When_ValidStartHourAndInvalidEndHour(int startHour, int endHour)
    {
        Action action = () =>
        {
            Duration duration = new(startHour, endHour);
        };

        action.Should().Throw<InvalidDurationException>();
    }

    [Theory]
    [InlineData(5, 23, "5 AM - 11 PM")]
    [InlineData(1, 23, "1 AM - 11 PM")]
    [InlineData(0, 23, "12 AM - 11 PM")]
    [InlineData(0, 1, "12 AM - 1 AM")]
    [InlineData(22, 23, "10 PM - 11 PM")]
    public void Should_Return_FormatedString_When_ValidStartHourAndValidEndHour(int startHour, int endHour, string expectedResult)
    {
        Duration duration = new(startHour, endHour);

        string? actualResult = duration.GetDurationInFormat();

        actualResult.Should().Be(expectedResult);
    }
}
