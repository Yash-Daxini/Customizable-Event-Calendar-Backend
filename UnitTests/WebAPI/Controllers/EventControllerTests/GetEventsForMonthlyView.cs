﻿using AutoMapper;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class GetEventsForMonthlyView : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public GetEventsForMonthlyView(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_Return_Events_When_EventOfCurrentMonth()
    {
        IActionResult actionResult = await _eventController.GetEventsForMonthlyView(1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _eventService.GetEventsForMonthlyViewByUserId(1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetEventsForMonthlyView(1);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
