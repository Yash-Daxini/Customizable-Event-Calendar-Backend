﻿using AutoMapper;
using Core.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class GetAllEventsCreatedByUser : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public GetAllEventsCreatedByUser(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_ReturnEvents_When_EventOfCurrentMonth()
    {
        List<EventResponseDto> events = [];

        IActionResult actionResult = await _eventController.GetAllEventsCreatedByUser(1);

        var returnedResult = Assert.IsType<OkObjectResult>(actionResult);

        Assert.Equivalent(events, returnedResult.Value);
    }

    [Fact]
    public async Task Should_ReturnServerError_When_SomeErrorOccurred()
    {
        _eventService.GetAllEventCreatedByUser(1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetAllEventsCreatedByUser(1);

        Assert.IsType<ObjectResult>(actionResult);
    }
}
