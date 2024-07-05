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

public class UpdateNonRecurringEvent : IClassFixture<AutoMapperFixture>
{
    private readonly IEventService _eventService;
    private readonly IMapper _mapper;
    private readonly EventController _eventController;

    public UpdateNonRecurringEvent(AutoMapperFixture autoMapperFixture)
    {
        _mapper = autoMapperFixture.Mapper;
        _eventService = Substitute.For<IEventService>();
        _eventController = new(_eventService, _mapper);
    }

    [Fact]
    public async Task Should_UpdateEvent_When_EventNotOverlaps()
    {
        NonRecurringEventRequestDto nonRecurringEventRequestDto = Substitute.For<NonRecurringEventRequestDto>();

        IActionResult actionResult = await _eventController.UpdateNonRecurringEvent(1, nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<CreatedAtActionResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_EventOverlaps()
    {
        Event eventObj = Substitute.For<Event>();

        NonRecurringEventRequestDto nonRecurringEventRequestDto = Substitute.For<NonRecurringEventRequestDto>();

        _eventService.UpdateEvent(eventObj, 1).ThrowsAsyncForAnyArgs(new EventOverlapException("Overlap"));

        IActionResult actionResult = await _eventController.UpdateNonRecurringEvent(1, nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<BadRequestObjectResult>();
    }

    [Fact]
    public async Task Should_Return_BadRequest_When_EventNotAvailableWithId()
    {
        Event eventObj = Substitute.For<Event>();

        NonRecurringEventRequestDto nonRecurringEventRequestDto = Substitute.For<NonRecurringEventRequestDto>();

        _eventService.UpdateEvent(eventObj, 1).ThrowsForAnyArgs(new NotFoundException(""));

        IActionResult actionResult = await _eventController.UpdateNonRecurringEvent(1, nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<NotFoundObjectResult>();
    }

    [Fact]
    public async Task Should_Return_ServerError_When_SomeErrorOccurred()
    {
        Event eventObj = Substitute.For<Event>();

        NonRecurringEventRequestDto nonRecurringEventRequestDto = Substitute.For<NonRecurringEventRequestDto>();

        _eventService.UpdateEvent(eventObj, 1).ThrowsAsyncForAnyArgs<Exception>();

        IActionResult actionResult = await _eventController.UpdateNonRecurringEvent(1, nonRecurringEventRequestDto);

        actionResult.Should().BeOfType<ObjectResult>();
    }
}
