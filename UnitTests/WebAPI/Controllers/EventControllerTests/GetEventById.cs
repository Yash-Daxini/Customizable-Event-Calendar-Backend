using AutoMapper;
using Core.Entities;
using Core.Exceptions;
using Core.Interfaces.IServices;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using WebAPI.Controllers;
using WebAPI.Dtos;

namespace UnitTests.WebAPI.Controllers.EventControllerTests;

public class GetEventById : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public GetEventById(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_Return_Event_When_EventAvailableWithId()
    {
        Event eventObj = Substitute.For<Event>();

        _eventService.GetEventById(1, 1).ReturnsForAnyArgs(eventObj);

        IActionResult actionResult = await _eventController.GetEventById(1, 1);

        actionResult.Should().BeOfType<OkObjectResult>();
    }

    [Fact]
    public async Task Should_Return_NotFound_When_EventNotAvailableWithId()
    {
        _eventService.GetEventById(1, 1).Throws(new NotFoundException(""));

        IActionResult actionResult = await _eventController.GetEventById(1, 1);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _eventService.GetEventById(1, 1).Throws<Exception>();

        IActionResult actionResult = await _eventController.GetEventById(1, 1);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
