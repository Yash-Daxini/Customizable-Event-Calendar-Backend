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

public class AddRecurringEvent : IClassFixture<AutoMapperFixture>
{

    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;
    private readonly Event _eventObj;
    private readonly RecurringEventRequestDto _recurringEventRequestDto;

    public AddRecurringEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
        _eventObj = Substitute.For<Event>();
        _recurringEventRequestDto = Substitute.For<RecurringEventRequestDto>();
    }

    [Fact]
    public async Task Should_AddRecurringEvent_When_EventNotOverlapsAndEventNotOverlap()
    {
        _eventService.AddEvent(_eventObj, 1).ReturnsForAnyArgs(1);

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        actionResult.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_EventOverlaps()
    {
        _eventService.AddEvent(_eventObj, 1).ThrowsAsyncForAnyArgs(new EventOverlapException("Overlap"));

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        _eventService.AddEvent(_eventObj, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.AddRecurringEvent(1, _recurringEventRequestDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
