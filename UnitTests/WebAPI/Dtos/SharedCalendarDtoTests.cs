﻿using WebAPI.Dtos;
using WebAPI.Validators;

namespace UnitTests.WebAPI.Dtos;

public class SharedCalendarDtoTests
{
    SharedCalendarDtoValidator _validator;

    public SharedCalendarDtoTests()
    {
        _validator = new();
    }

    [Fact]
    public void Should_ReturnFalse_When_InvalidSharedCalendarDto()
    {
        SharedCalendarDto sharedCalendarDto = new ();  

        var result = _validator.Validate(sharedCalendarDto);

        Assert.False(result.IsValid);
    }

    [Fact]
    public void Should_ReturnTrue_When_ValidSharedCalendarDto()
    {
        SharedCalendarDto sharedCalendarDto = new ()
        {
            Id = 1,
            SenderUserId = 1,
            ReceiverUserId = 2,
            FromDate = new DateOnly(2024,4,1),
            ToDate = new DateOnly(2024,4,1),
        };

        var result = _validator.Validate(sharedCalendarDto);

        Assert.True(result.IsValid);
    }
}